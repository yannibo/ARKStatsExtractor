﻿using ARKBreedingStats.Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ARKBreedingStats
{
    public partial class NotesControl : UserControl
    {
        private List<Note> noteList;
        private Note selectedNote;
        public event Form1.CollectionChangedEventHandler changed;

        public NotesControl()
        {
            InitializeComponent();
        }

        public List<Note> NoteList
        {
            //get { return noteList; }
            set
            {
                listViewNoteTitles.Items.Clear();
                richTextBoxNote.Text = string.Empty;
                noteList = value;
                if (value != null)
                {
                    foreach (Note n in value)
                    {
                        ListViewItem lvi = new ListViewItem(n.Title)
                        {
                            Tag = n
                        };
                        listViewNoteTitles.Items.Add(lvi);
                    }
                }
            }
        }

        private void listViewNoteTitles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewNoteTitles.SelectedIndices.Count > 0)
            {
                selectedNote = (Note)listViewNoteTitles.SelectedItems[0].Tag;
                tbNoteTitle.Text = selectedNote.Title;
                richTextBoxNote.Text = selectedNote.Text;
            }
        }

        public void AddNote()
        {
            Note n = new Note("<new note>");
            noteList.Add(n);
            ListViewItem lvi = new ListViewItem(n.Title)
            {
                Tag = n
            };
            listViewNoteTitles.Items.Add(lvi);
            listViewNoteTitles.Items[listViewNoteTitles.Items.Count - 1].Selected = true;
            tbNoteTitle.Focus();
            tbNoteTitle.SelectAll();
        }

        public void RemoveSelectedNote()
        {
            if (listViewNoteTitles.SelectedItems.Count > 0
                    && MessageBox.Show($"Delete note with the title \"{((Note)(listViewNoteTitles.SelectedItems[0].Tag)).Title}\"?",
                            "Delete Note?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Note n = (Note)listViewNoteTitles.SelectedItems[0].Tag;
                noteList.Remove(n);
                listViewNoteTitles.Items.Remove(listViewNoteTitles.SelectedItems[0]);
                if (listViewNoteTitles.Items.Count > 0)
                    listViewNoteTitles.Items[0].Selected = true;
                else
                {
                    richTextBoxNote.Text = string.Empty;
                    tbNoteTitle.Text = string.Empty;
                }
                changed?.Invoke();
            }
        }

        private void richTextBoxNote_Leave(object sender, EventArgs e)
        {
            if (selectedNote != null)
            {
                selectedNote.Text = richTextBoxNote.Text;
                changed?.Invoke();
            }
        }

        private void tbNoteTitle_Leave(object sender, EventArgs e)
        {
            if (selectedNote != null)
            {
                if (listViewNoteTitles.SelectedIndices.Count > 0)
                {
                    listViewNoteTitles.SelectedItems[0].Text = tbNoteTitle.Text;
                }
                selectedNote.Title = tbNoteTitle.Text;
                changed?.Invoke();
            }
        }

        private void listViewNoteTitles_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            // display all checked notes in the overlay
            if (ARKOverlay.theOverlay == null)
                return;

            var sb = new StringBuilder();

            foreach (var le in listViewNoteTitles.CheckedItems)
            {
                var note = (Note)((ListViewItem)le).Tag;

                sb.Append("\n\n" + note.Title + ": " + note.Text);
            }
            ARKOverlay.theOverlay.SetNotes(sb.ToString());
        }
    }
}
