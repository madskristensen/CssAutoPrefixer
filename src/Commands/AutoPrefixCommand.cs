using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using System;
using System.Threading.Tasks;

namespace CssAutoPrefixer
{
    internal sealed class AutoPrefixCommand : BaseCommand
    {
        private Guid _commandGroup = PackageGuids.guidPackageCmdSet;
        private const uint _commandId = PackageIds.AutoPrefixCommandId;

        private IWpfTextView _view;
        private ITextBufferUndoManager _undoManager;
        private NodeProcess _node;

        public AutoPrefixCommand(IWpfTextView view, ITextBufferUndoManager undoManager, NodeProcess node)
        {
            _view = view;
            _undoManager = undoManager;
            _node = node;
        }

        public override int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            if (pguidCmdGroup == _commandGroup && nCmdID == _commandId)
            {
                if (_node != null && _node.IsReadyToExecute())
                {
                    ThreadHelper.JoinableTaskFactory.RunAsync(ExecuteAsync);
                }

                return VSConstants.S_OK;
            }

            return Next.Exec(pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
        }

        private async Task<bool> ExecuteAsync()
        {
            string input = _view.TextBuffer.CurrentSnapshot.GetText();
            string output = await _node.ExecuteProcess(input);

            if (string.IsNullOrEmpty(output) || input == output)
                return false;

            using (ITextEdit edit = _view.TextBuffer.CreateEdit())
            using (ITextUndoTransaction undo = _undoManager.TextBufferUndoHistory.CreateTransaction(Vsix.Name))
            {
                edit.Replace(0, _view.TextBuffer.CurrentSnapshot.Length, output);
                edit.Apply();

                var dte = (DTE)ServiceProvider.GlobalProvider.GetService(typeof(DTE));
                dte.ExecuteCommand("Edit.FormatDocument");

                undo.Complete();
            }

            return true;
        }

        public override int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            if (pguidCmdGroup == _commandGroup && prgCmds[0].cmdID == _commandId)
            {
                if (_node != null && _node.IsReadyToExecute())
                {
                    prgCmds[0].cmdf = (uint)OLECMDF.OLECMDF_ENABLED | (uint)OLECMDF.OLECMDF_SUPPORTED;
                }
                else
                {
                    prgCmds[0].cmdf = (uint)OLECMDF.OLECMDF_SUPPORTED;
                }

                return VSConstants.S_OK;
            }

            return Next.QueryStatus(pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }
    }
}