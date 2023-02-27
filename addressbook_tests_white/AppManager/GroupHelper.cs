using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestStack.White.InputDevices;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TreeItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;
using System.Windows.Automation;

namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";
        public static string DELETEGROUPWINTITLE = "Delete group";
        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> grouplist = new List<GroupData>();
            Window dialogue = OpenGroupDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            foreach (TreeNode item in root.Nodes)
            {
                grouplist.Add(new GroupData()
                {
                    Name = item.Text
                });
            }
            CloseGroupDialogue(dialogue);
            return grouplist;
        }
        public void CheckGroupExist()
        {
            if (GetGroupCount() <= 1)
            {
                GroupData newGroup = new GroupData()
                {
                    Name = "GroupForTest"
                };
                AddGroup(newGroup);
            }
        }

        public int GetGroupCount()
        {
            Window dialogue = OpenGroupDialogue();
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            int count = root.Nodes.Count();
            CloseGroupDialogue(dialogue);
            return count;
        }

        public void Remove(int index)

        {
            Window dialogue = OpenGroupDialogue();

            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");
            TreeNode root = tree.Nodes[0];
            root.Nodes[index].Select();
            Window deleteDialogue = OpenDeletGroupDialog(dialogue);
            SubmitGroupDelete(deleteDialogue);
            CloseGroupDialogue(dialogue);

        }
        public void AddGroup(GroupData newGroup)
        {
            Window dialogue = OpenGroupDialogue();

            dialogue.Get<Button>("uxNewAddressButton").Click();
            TextBox textBox = (TextBox)dialogue.Get(SearchCriteria.ByControlType(ControlType.Edit));
            textBox.Enter(newGroup.Name);
            Keyboard.Instance.PressSpecialKey(KeyboardInput.SpecialKeys.RETURN);
            CloseGroupDialogue(dialogue);

        }

        private void CloseGroupDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();
        }

        private Window OpenGroupDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);
        }

        private Window OpenDeletGroupDialog(Window dialogue)
        {
            dialogue.Get<Button>("uxDeleteAddressButton").Click();
            return dialogue.ModalWindow(DELETEGROUPWINTITLE);
        }

        private void SubmitGroupDelete(Window dialogue)
        {
            dialogue.Get<Button>("uxOKAddressButton").Click();
        }
    }
}