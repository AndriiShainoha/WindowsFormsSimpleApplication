using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{

    public partial class StudentEditor : Form
    {
        List<Student> obj;
        public StudentEditor()
        {
            InitializeComponent();
            //imageList1.Images.Add(Image.FromFile(@"C:\Users\andri\Desktop\shutterstock_160918730.jpg"));
            obj = new List<Student>();
            toolTip1.SetToolTip(this.button1, "Read and add information from file to List");
            toolTip2.SetToolTip(this.button4, "Write information from file to ListView");
            toolTip3.SetToolTip(this.button3, "Sorting and Deleting by task");
            toolTip4.SetToolTip(this.button2, "Write information to a file");
            toolTip5.SetToolTip(this.button5, "Clear items from ListView");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  //з файлами всьо работає
        {   
        }
        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string writePath = @"C:\Users\andri\Desktop\ПЗ\ВІПЗ\Лаби ВІПЗ\4 Лаба програми\textout.txt";
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                foreach (var d in obj)
                {
                    sw.Write(d.Surname+" ");
                    sw.Write(d.Name + " ");
                    sw.Write(d.Date[0] + " ");
                    sw.Write(d.Date[1] + " ");
                    sw.Write(d.Date[2] + " ");
                    sw.Write(d.Marks[0] + " ");
                    sw.Write(d.Marks[1] + " ");
                    sw.Write(d.Marks[2] + " ");
                    sw.WriteLine();
                }
                sw.Close();
            }

        }

        private void sortAndDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string readPath = @"C:\Users\andri\Desktop\ПЗ\ВІПЗ\Лаби ВІПЗ\4 Лаба програми\textin.txt";

            using (var sr = new StreamReader(readPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var input = sr.ReadLine();
                    var regex = new Regex(@"^([a-zA-Z]+) ([a-zA-Z]+) (\d+) (\d+) (\d+) (\d+) (\d+) (\d+)$");
                    if (regex.IsMatch(input))
                    {
                        var match = regex.Match(input);
                        var groups = match.Groups;

                        var surname = groups[1].Value;
                        var firstname = groups[2].Value;

                        var date =
                            groups.Cast<Group>().Skip(3)
                            .Take(3)
                            .Select(g => int.Parse(g.Value))
                            .ToArray();

                        var marks =
                            groups.Cast<Group>().Skip(6)
                            .Take(3)
                            .Select(g => int.Parse(g.Value))
                            .ToArray();

                       var student = new Student(surname, firstname, date, marks);
                        obj.Add(student);
                    }
                    else
                    {
                        Console.WriteLine("Error with reading");                  //переписати  під норм вивід
                    } 

                }
                sr.Close();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox1.Text);
            obj.Sort();

            obj.RemoveAll(x =>
            {
                for (int i = 0; i < 3; ++i) if (x.Marks[i] == n) return true;
                return false;
            });
            listView1.Items.Clear();
            foreach (var students in obj)
            {
                var listvi = new ListViewItem(students.Surname);
                listvi.SubItems.Add(students.Name);
                listvi.SubItems.Add(students.Date[0].ToString());
                listvi.SubItems.Add(students.Date[1].ToString());
                listvi.SubItems.Add(students.Date[2].ToString());
                listvi.SubItems.Add(students.Marks[0].ToString());
                listvi.SubItems.Add(students.Marks[1].ToString());
                listvi.SubItems.Add(students.Marks[2].ToString());
                listView1.Items.Add(listvi);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (var students in obj)
            {
                var listvi = new ListViewItem(students.Surname);
                listvi.SubItems.Add(students.Name);
                listvi.SubItems.Add(students.Date[0].ToString());
                listvi.SubItems.Add(students.Date[1].ToString());
                listvi.SubItems.Add(students.Date[2].ToString());
                listvi.SubItems.Add(students.Marks[0].ToString());
                listvi.SubItems.Add(students.Marks[1].ToString());
                listvi.SubItems.Add(students.Marks[2].ToString());
                listView1.Items.Add(listvi);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
    public class Student : IComparable<Student>
    {
        public string Surname;
        public string Name;
        public int[] Date = new int[3];
        public int[] Marks = new int[3];
        public Student(string s,string n,int[]d,int []m)
        {
            Surname = s;
            Name = n;
            Date = d;
            Marks = m;
        }

        public int CompareTo(Student other)
        {
            if (this.Date[2] > other.Date[2])
            {
                return 1;
            }
            else if (this.Date[2] == other.Date[2])
            {
                if (this.Date[1] > other.Date[1])
                {
                    return 1;
                }
                else if (this.Date[1] == other.Date[1])
                {
                    if (this.Date[0] > other.Date[0])
                    {
                        return 1;
                    }
                    else if(this.Date[0] == other.Date[0])
                    {
                        return 0;
                    }
                }
            }
            return -1;
        }
    }

}
