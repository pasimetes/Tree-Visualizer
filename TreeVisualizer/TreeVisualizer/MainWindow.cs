using System;
using System.Linq;
using System.Windows.Forms;

namespace TreeVisualizer
{
    public partial class MainWindow : Form
    {
        private DrawBox _bstDrawBox;
        private DrawBox _avlDrawBox;

        private ITree<int> _binarSearchTree;
        private ITree<int> _avlTree;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            _bstDrawBox = new DrawBox
            {
                Dock = DockStyle.Fill,
            };
            _avlDrawBox = new DrawBox
            {
                Dock = DockStyle.Fill
            };

            _binarSearchTree = new BinarySearchTree<int>(new TreeConfiguration(circleDiameter: 45, arrowAnchorSize: 5));
            _avlTree = new AVLTree<int>(new TreeConfiguration(circleDiameter: 45, arrowAnchorSize: 5));

            tabPage_BST.Controls.Add(_bstDrawBox);
            tabPage_AVL.Controls.Add(_avlDrawBox);
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Insert.Text))
            {
                MessageBox.Show("You forgot to enter a value ;)", "Reminder");
                return;
            }
            if (!int.TryParse(txt_Insert.Text, out int value))
            {
                MessageBox.Show($"Expected value type of {typeof(int)}", "Error");
                return;
            }

            if (tabControl.SelectedTab == tabPage_BST)
            {
                _binarSearchTree.Insert(value);
                _bstDrawBox.Print<BinarySearchTree<int>, int>(_binarSearchTree);
            }
            else if (tabControl.SelectedTab == tabPage_AVL)
            {
                _avlTree.Insert(value);
                _avlDrawBox.Print<AVLTree<int>, int>(_avlTree);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Remove.Text))
            {
                MessageBox.Show("You forgot to enter a value ;)", "Reminder");
                return;
            }
            if (!int.TryParse(txt_Remove.Text, out int value))
            {
                MessageBox.Show($"Expected value type of {typeof(int)}", "Error");
                return;
            }

            if (tabControl.SelectedTab == tabPage_BST)
            {
                _binarSearchTree.Remove(value);
                _bstDrawBox.Print<BinarySearchTree<int>, int>(_binarSearchTree);
            }
            else if (tabControl.SelectedTab == tabPage_AVL)
            {
                _avlTree.Remove(value);
                _avlDrawBox.Print<AVLTree<int>, int>(_avlTree);
            }
        }
    }
}
