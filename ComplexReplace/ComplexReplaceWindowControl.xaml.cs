//------------------------------------------------------------------------------
// <copyright file="ComplexReplaceWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;

namespace ComplexReplace
{
    public partial class ComplexReplaceWindowControl : UserControl
    {
        #region Fields
        private TextDocument _document;
        #endregion

        #region Properties
        [Import]
        public ITextDocumentFactoryService textDocumentFactory { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        ///     Initializes a new instance of the <see cref="ComplexReplaceWindowControl" /> class.
        /// </summary>
        public ComplexReplaceWindowControl()
        {
            InitializeComponent();
            btnUndo.IsEnabled = false;

            txtFind.Text = Properties.Settings.Default.Find;
            txtReplace.Text = Properties.Settings.Default.Replace;
        }
        #endregion

        #region Methods
        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Find = txtFind.Text;
            Properties.Settings.Default.Replace = txtReplace.Text;
            Properties.Settings.Default.Save();

            var doc = (Package.GetGlobalService(typeof (DTE)) as DTE).ActiveDocument;
            if (doc == null)
            {
                MessageBox.Show("No file opened");
                return;
            }

            _document = doc.Object() as TextDocument;
            
            var findStrCollection = txtFind.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var replaceStrCollection = txtReplace.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            btnUndo.IsEnabled = FindAndReplace(findStrCollection, replaceStrCollection);
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            var doc = (Package.GetGlobalService(typeof (DTE)) as DTE).ActiveDocument;
            var txtDoc = doc.Object() as TextDocument;
            if (txtDoc == _document)
            {
                var findStrCollection = txtFind.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                var replaceStrCollection = txtReplace.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                btnUndo.IsEnabled = !FindAndReplace(replaceStrCollection, findStrCollection);
            }
        }

        private bool FindAndReplace(string[] findStrCollection, string[] replaceStrCollection)
        {
            if (findStrCollection.Length == 0)
            {
                MessageBox.Show("No search arguments provided");
                return false;
            }

            if (findStrCollection.Length != replaceStrCollection.Length)
            {
                MessageBox.Show("Number of search and replace arguments mismatch");
                return false;
            }

            for (var i = 0; i < findStrCollection.Length; i++)
            {
                var editPnt = _document.StartPoint.CreateEditPoint();
                editPnt.ReplacePattern(_document.EndPoint, findStrCollection[i], replaceStrCollection[i]);
            }

            return true;
        }
        #endregion
    }
}