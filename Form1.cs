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

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Book_Keeper
{
    public partial class Form1 : Form
    {
        public MySqlConnection conn { get; set; }
        public DataGridView show_text { get; set; }
        public DataGridView search_by_name_text { get; set; }
        public DataGridView search_by_tags_text { get; set; }
        public string from { get; set; }
        public string what { get; set; }
        public string user { get; set; }

        public Form1()
        {
            InitializeComponent();

            //creating panels

            interaction();
            show_tables();
            show_res();
            search_by();
            search_by_tags_tables();
            search_by_tags_res();
            search_by_name_tables();
            search_by_name_res();
            insert_tables();
            insert_books_inp();
            insert_planned_inp();
            insert_reading_inp();
            insert_dropped_inp();
            insert_book_tags_inp();
            insert_tags_inp();
            update_tables();
            update_books_val();
            update_planned_val();
            update_reading_val();
            update_dropped_val();
            update_tags_val();
            update_book_tags_val();
            update_res();
            delete_tables();
            delete_books_res();
            delete_planned_res();
            delete_reading_res();
            delete_dropped_res();
            delete_tags_res();
            delete_book_tags_res();
            login_inp();
            logged_as();
            backup();

            //Connecting to db

            string myConnectionString = "server=127.0.0.1; port=3307; uid=guest; pwd=; database=bookkeeper";
            conn = new MySqlConnection(myConnectionString);

            try
            {
                this.Controls["logged_as"].Visible = true;
                this.Controls["logged_as"].Controls["logged_as_l"].Text = "Logged in as guest.";
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
            //conn.Close();
            //MessageBox.Show("Connection closed.");
        }

        public void interaction() // Creating panel containing interaction buttons
        {
            Panel interaction = Create_Panel(new Point(10, 10), new Size(1465, 46), "interaction");
            interaction.Visible = true;

            Button show = Create_button("Show", interaction, 0, new EventHandler(interaction0_Click));
            Button search = Create_button("Search", interaction, 1, new EventHandler(interaction1_Click));
            Button insert = Create_button("Insert", interaction, 2, new EventHandler(interaction2_Click));
            Button update = Create_button("Update", interaction, 3, new EventHandler(interaction3_Click));
            Button delete = Create_button("Delete", interaction, 4, new EventHandler(interaction4_Click));
            Button login = Create_button("Login", interaction, 5, new EventHandler(interaction5_Click));
            Button admin = Create_button("backup", interaction, 6, new EventHandler(interaction6_Click));
        }

        public void show_tables() //Panel for show table options
        {
            Panel show_tables = Create_Panel(new Point(10, 66), new Size(1465, 46), "show_tables");

            Button show_books = Create_button("From books", show_tables, 0, new EventHandler(show_tables0_Click));
            Button show_books_with_tags = Create_button("From book with tags", show_tables, 1, new EventHandler(show_tables1_Click));
            Button show_planned = Create_button("From planned", show_tables, 2, new EventHandler(show_tables2_Click));
            Button show_planned_with_tags = Create_button("From planned with tags", show_tables, 3, new EventHandler(show_tables3_Click));
            Button show_reading = Create_button("From reading", show_tables, 4, new EventHandler(show_tables4_Click));
            Button show_dropped = Create_button("From dropped", show_tables, 5, new EventHandler(show_tables5_Click));
            Button show_tags = Create_button("From tags", show_tables, 6, new EventHandler(show_tables6_Click));
        }

        public void show_res() //Panel for show results
        {
            Panel show_res = Create_Panel(new Point(10, 122), new Size(1465, 520), "show_res");

            show_text = Create_DataGridView(new Point(10, 10), new Size(1445, 500), "show_text", show_res);
        }

        public void search_by() //Panel for search by options
        {
            Panel search_by = Create_Panel(new Point(10, 66), new Size(1465, 46), "search_by");

            Button search_by_name = Create_button("By Name", search_by, 0, new EventHandler(search_by0_Click));
            Button search_by_tags = Create_button("By Tags", search_by, 1, new EventHandler(search_by1_Click));
        }

        public void search_by_tags_tables() //Panel for search table options by tags
        {
            Panel search_by_tags_tables = Create_Panel(new Point(10, 122), new Size(1465, 46), "search_by_tags_tables");

            Button search_by_tags_books = Create_button("From Books", search_by_tags_tables, 0, new EventHandler(search_by_tags_tables0_Click));
            Button search_by_tags_planned = Create_button("From Planned", search_by_tags_tables, 1, new EventHandler(search_by_tags_tables1_Click));
        }

        public void search_by_tags_res() //Panel for input and output of search table options by tags
        {
            Panel search_by_tags_res = Create_Panel(new Point(10, 178), new Size(1465, 460), "search_by_tags_res");

            Label search_by_tags_l = Create_Label(new Point(10, 10), "search_by_tags_l", search_by_tags_res);
            TextBox search_by_tags_inp = Create_TextBox(new Point(10, 35), "search_by_tags_inp", search_by_tags_res);
            Button search_by_tags_go = Create_button("Search", search_by_tags_res, 0, new EventHandler(search_by_tags_res0_Click), new Point(732, 68));
            search_by_tags_text = Create_DataGridView(new Point(10, 122), new Size(1445, 330), "search_by_tags_text", search_by_tags_res);
        }

        public void search_by_name_tables() //Panel for search table options by name
        {
            Panel search_by_name_tables = Create_Panel(new Point(10, 122), new Size(1465, 46), "search_by_name_tables");

            Button search_by_name_books = Create_button("From Books", search_by_name_tables, 0, new EventHandler(search_by_name_tables0_Click));
            Button search_by_name_planned = Create_button("From Planned", search_by_name_tables, 1, new EventHandler(search_by_name_tables1_Click));
            Button search_by_name_reading = Create_button("From Reading", search_by_name_tables, 2, new EventHandler(search_by_name_tables2_Click));
            Button search_by_name_dropped = Create_button("From Dropped", search_by_name_tables, 3, new EventHandler(search_by_name_tables3_Click));
        }

        public void search_by_name_res() //Panel for input and output of search table options by name
        {
            Panel search_by_name_res = Create_Panel(new Point(10, 178), new Size(1465, 460), "search_by_name_res");

            Label search_by_name_l = Create_Label(new Point(10, 10), "search_by_name_l", search_by_name_res);
            TextBox search_by_name_inp = Create_TextBox(new Point(10, 35), "search_by_name_inp", search_by_name_res);
            Button search_by_name_go = Create_button("Search", search_by_name_res, 0, new EventHandler(search_by_name_res0_Click), new Point(732, 68));
            search_by_name_text = Create_DataGridView(new Point(10, 122), new Size(1445, 330), "search_by_name_text", search_by_name_res);
        }

        public void insert_tables() //Panel for choosing table in insert
        {
            Panel insert_tables = Create_Panel(new Point(10, 66), new Size(1465, 46), "insert_tables");

            Button insert_books = Create_button("Into books", insert_tables, 0, new EventHandler(insert_tables0_Click));
            Button insert_planned = Create_button("Into planned", insert_tables, 1, new EventHandler(insert_tables1_Click));
            Button insert_reading = Create_button("From planned to reading", insert_tables, 2, new EventHandler(insert_tables2_Click));
            Button insert_dropped = Create_button("From reading to dropped", insert_tables, 3, new EventHandler(insert_tables3_Click));
            Button insert_tags = Create_button("New tag", insert_tables, 4, new EventHandler(insert_tables4_Click));
            Button insert_book_tags = Create_button("Add tag to book", insert_tables, 5, new EventHandler(insert_tables5_Click));
        }

        public void insert_books_inp() //Panel for inserting a single value into books
        {
            Panel insert_books_inp = Create_Panel(new Point(10, 122), new Size(1465, 412), "insert_books_inp");

            Label insert_books_name_l = Create_Label(new Point(10, 10), "insert_books_name_l", insert_books_inp);
            TextBox insert_books_name_text = Create_TextBox(new Point(10, 35), "insert_books_name_text", insert_books_inp);
            Label insert_books_author_l = Create_Label(new Point(10, 68), "insert_books_author_l", insert_books_inp);
            TextBox insert_books_author_text = Create_TextBox(new Point(10, 93), "insert_books_author_text", insert_books_inp);
            Label insert_books_chapters_l = Create_Label(new Point(10, 126), "insert_books_chapters_l", insert_books_inp);
            TextBox insert_books_chapters_text = Create_TextBox(new Point(10, 151), "insert_books_chapters_text", insert_books_inp);
            Label insert_books_pages_l = Create_Label(new Point(10, 184), "insert_books_pages_l", insert_books_inp);
            TextBox insert_books_pages_text = Create_TextBox(new Point(10, 209), "insert_books_pages_text", insert_books_inp);
            Label insert_books_raitng_l = Create_Label(new Point(10, 242), "insert_books_rating_l", insert_books_inp);
            TextBox insert_books_rating_text = Create_TextBox(new Point(10, 267), "insert_books_rating_text", insert_books_inp);
            Label insert_books_link_l = Create_Label(new Point(10, 300), "insert_books_link_l", insert_books_inp);
            TextBox insert_books_link_text = Create_TextBox(new Point(10, 325), "insert_books_link_text", insert_books_inp);
            Button insert_books_go = Create_button("Insert", insert_books_inp, 0, new EventHandler(insert_books_inp0_Click), new Point(732, 358));
        }

        public void insert_planned_inp() //Panel for inserting a single value into planned
        {
            Panel insert_planned_inp = Create_Panel(new Point(10, 122), new Size(1465, 180), "insert_planned_inp");

            Label insert_planned_name_l = Create_Label(new Point(10, 10), "insert_planned_name_l", insert_planned_inp);
            TextBox insert_planned_name_text = Create_TextBox(new Point(10, 35), "insert_planned_name_text", insert_planned_inp);
            Label insert_planned_interest_l = Create_Label(new Point(10, 68), "insert_planned_interest_l", insert_planned_inp);
            TextBox insert_planned_interest_text = Create_TextBox(new Point(10, 93), "insert_planned_interest_text", insert_planned_inp);
            Button insert_planned_go = Create_button("Insert", insert_planned_inp, 0, new EventHandler(insert_planned_inp0_Click), new Point(732, 126));
        }

        public void insert_reading_inp() //Panel for inserting a single value into reading
        {
            Panel insert_reading_inp = Create_Panel(new Point(10, 122), new Size(1465, 180), "insert_reading_inp");

            Label insert_reading_name_l = Create_Label(new Point(10, 10), "insert_reading_name_l", insert_reading_inp);
            TextBox insert_reading_name_text = Create_TextBox(new Point(10, 35), "insert_reading_name_text", insert_reading_inp);
            Label insert_reading_progress_l = Create_Label(new Point(10, 68), "insert_reading_progress_l", insert_reading_inp);
            TextBox insert_reading_progress_text = Create_TextBox(new Point(10, 93), "insert_reading_progress_text", insert_reading_inp);
            Button insert_reading_go = Create_button("Insert", insert_reading_inp, 0, new EventHandler(insert_reading_inp0_Click), new Point(732, 126));
        }

        public void insert_dropped_inp() //Panel for inserting a single value into dropped
        {
            Panel insert_dropped_inp = Create_Panel(new Point(10, 122), new Size(1465, 180), "insert_dropped_inp");

            Label insert_dropped_name_l = Create_Label(new Point(10, 10), "insert_dropped_name_l", insert_dropped_inp);
            TextBox insert_dropped_name_text = Create_TextBox(new Point(10, 35), "insert_dropped_name_text", insert_dropped_inp);
            Label insert_dropped_reason_l = Create_Label(new Point(10, 68), "insert_dropped_reason_l", insert_dropped_inp);
            TextBox insert_dropped_reason_text = Create_TextBox(new Point(10, 93), "insert_dropped_reason_text", insert_dropped_inp);
            Button insert_dropped_go = Create_button("Insert", insert_dropped_inp, 0, new EventHandler(insert_dropped_inp0_Click), new Point(732, 126));
        }

        public void insert_tags_inp() //Panel for inserting a single value into tags
        {
            Panel insert_tags_inp = Create_Panel(new Point(10, 122), new Size(1465, 122), "insert_tags_inp");

            Label insert_tags_name_l = Create_Label(new Point(10, 10), "insert_tags_name_l", insert_tags_inp);
            TextBox insert_tags_name_text = Create_TextBox(new Point(10, 35), "insert_tags_name_text", insert_tags_inp);
            Button insert_tags_go = Create_button("Insert", insert_tags_inp, 0, new EventHandler(insert_tags_inp0_Click), new Point(732, 68));
        }

        public void insert_book_tags_inp() //Panel for inserting a single value into book_tags
        {
            Panel insert_book_tags_inp = Create_Panel(new Point(10, 122), new Size(1465, 180), "insert_book_tags_inp");

            Label insert_book_tags_name_l = Create_Label(new Point(10, 10), "insert_book_tags_name_l", insert_book_tags_inp);
            TextBox insert_book_tags_name_text = Create_TextBox(new Point(10, 35), "insert_book_tags_name_text", insert_book_tags_inp);
            Label insert_book_tags_tag_l = Create_Label(new Point(10, 68), "insert_book_tags_tag_l", insert_book_tags_inp);
            TextBox insert_book_tags_tag_text = Create_TextBox(new Point(10, 93), "insert_book_tags_tag_text", insert_book_tags_inp);
            Button insert_book_tags_go = Create_button("Insert", insert_book_tags_inp, 0, new EventHandler(insert_book_tags_inp0_Click), new Point(732, 126));
        }

        public void update_tables() //Panel for choosing table in update
        {
            Panel update_tables = Create_Panel(new Point(10, 66), new Size(1465, 46), "update_tables");

            Button update_books = Create_button("Books", update_tables, 0, new EventHandler(update_tables0_Click));
            Button update_planned = Create_button("Planned", update_tables, 1, new EventHandler(update_tables1_Click));
            Button update_reading = Create_button("Reading", update_tables, 2, new EventHandler(update_tables2_Click));
            Button update_dropped = Create_button("Dropped", update_tables, 3, new EventHandler(update_tables3_Click));
            Button update_tags = Create_button("Tags", update_tables, 4, new EventHandler(update_tables4_Click));
            Button update_book_tags = Create_button("Tags of a book", update_tables, 5, new EventHandler(update_tables5_Click));
        }

        public void update_books_val() //Panel for updating books
        {
            Panel update_books_val = Create_Panel(new Point(10, 126), new Size(1465, 46), "update_books_val");

            Button update_books_name = Create_button("Change name of book", update_books_val, 0, new EventHandler(update_books_val0_Click));
            Button update_books_author = Create_button("Change name of author", update_books_val, 1, new EventHandler(update_books_val1_Click));
            Button update_books_chapters = Create_button("Change chapters count", update_books_val, 2, new EventHandler(update_books_val2_Click));
            Button update_books_pages = Create_button("Change pages count", update_books_val, 3, new EventHandler(update_books_val3_Click));
            Button update_books_rating = Create_button("Change book rating", update_books_val, 4, new EventHandler(update_books_val4_Click));
            Button update_books_link = Create_button("Change link to book", update_books_val, 5, new EventHandler(update_books_val5_Click));
        }

        public void update_planned_val() //Panel for updating planned books
        {
            Panel update_planned_val = Create_Panel(new Point(10, 126), new Size(1465, 46), "update_planned_val");

            Button update_planned_name = Create_button("Change name of book", update_planned_val, 0, new EventHandler(update_planned_val0_Click));
            Button update_planned_interest = Create_button("Change interest of book", update_planned_val, 1, new EventHandler(update_planned_val1_Click));
        }

        public void update_reading_val() //Panel for updating books currently being read
        {
            Panel update_reading_val = Create_Panel(new Point(10, 126), new Size(1465, 46), "update_reading_val");

            Button update_reading_name = Create_button("Change name of book", update_reading_val, 0, new EventHandler(update_reading_val0_Click));
            Button update_reading_progress = Create_button("Change progress of book", update_reading_val, 1, new EventHandler(update_reading_val1_Click));
        }

        public void update_dropped_val() //Panel for updating dropped books
        {
            Panel update_dropped_val = Create_Panel(new Point(10, 126), new Size(1465, 46), "update_dropped_val");

            Button update_dropped_name = Create_button("Change name of book", update_dropped_val, 0, new EventHandler(update_dropped_val0_Click));
            Button update_dropped_progress = Create_button("Change progress of book", update_dropped_val, 1, new EventHandler(update_dropped_val1_Click));
            Button update_dropped_reason = Create_button("Change reason for dropping", update_dropped_val, 2, new EventHandler(update_dropped_val2_Click));
        }

        public void update_tags_val() //Panel for changing tags
        {
            Panel update_tags_val = Create_Panel(new Point(10, 126), new Size(1465, 46), "update_tags_val");

            Button update_tags_name = Create_button("Change name of tag", update_tags_val, 0, new EventHandler(update_tags_val0_Click));
        }

        public void update_book_tags_val() //Panel for changing tags of book
        {
            Panel update_book_tags_val = Create_Panel(new Point(10, 126), new Size(1465, 46), "update_book_tags_val");

            Button update_book_tags_book = Create_button("Change tag to another book", update_book_tags_val, 0, new EventHandler(update_book_tags_val0_Click));
            Button update_book_tags_tag = Create_button("Change one tag for another", update_book_tags_val, 1, new EventHandler(update_book_tags_val1_Click));
        }

        public void update_res() //Panel for inserting update values
        {
            Panel update_res = Create_Panel(new Point(10, 182), new Size(1465, 180), "update_res");

            Label update_old_l = Create_Label(new Point(10, 10), "update_old_l", update_res);
            TextBox update_old_inp = Create_TextBox(new Point(10, 35), "update_old_inp", update_res);
            Label update_new_l = Create_Label(new Point(10, 68), "update_new_l", update_res);
            TextBox update_new_inp = Create_TextBox(new Point(10, 93), "update_new_inp", update_res);

            Button update_res_go = Create_button("Update", update_res, 0, new EventHandler(update_res0_Click), new Point(732, 126));
        }

        public void delete_tables() //Panel for choosing table to delete from
        {
            Panel delete_tables = Create_Panel(new Point(10, 66), new Size(1465, 46), "delete_tables");

            Button delete_books = Create_button("From books", delete_tables, 0, new EventHandler(delete_tables0_Click));
            Button delete_planned = Create_button("From planned", delete_tables, 1, new EventHandler(delete_tables1_Click));
            Button delete_reading = Create_button("From reading to dropped", delete_tables, 2, new EventHandler(delete_tables2_Click));
            Button delete_dropped = Create_button("From dropped", delete_tables, 3, new EventHandler(delete_tables3_Click));
            Button delete_tags = Create_button("From tags", delete_tables, 4, new EventHandler(delete_tables4_Click));
            Button delete_book_tags = Create_button("From tags of a book", delete_tables, 5, new EventHandler(delete_tables5_Click));
        }

        public void delete_books_res() //Panel for input of what to delete from books
        {
            Panel delete_books_res = Create_Panel(new Point(10, 122), new Size(1465, 122), "delete_books_res");

            Label delete_books_res_l = Create_Label(new Point(10, 10), "delete_books_res_l", delete_books_res);
            TextBox delete_books_res_inp = Create_TextBox(new Point(10, 35), "delete_books_res_inp", delete_books_res);
            Button delete_books_res_go = Create_button("Delete", delete_books_res, 0, new EventHandler(delete_books_res0_Click), new Point(732, 68));
        }

        public void delete_planned_res() //Panel for input of what to delete from planned
        {
            Panel delete_planned_res = Create_Panel(new Point(10, 122), new Size(1465, 122), "delete_planned_res");

            Label delete_planned_res_l = Create_Label(new Point(10, 10), "delete_planned_res_l", delete_planned_res);
            TextBox delete_planned_res_inp = Create_TextBox(new Point(10, 35), "delete_planned_res_inp", delete_planned_res);
            Button delete_planned_res_go = Create_button("Delete", delete_planned_res, 0, new EventHandler(delete_planned_res0_Click), new Point(732, 68));
        }

        public void delete_reading_res() //Panel for input of what to delete from reading
        {
            Panel delete_reading_res = Create_Panel(new Point(10, 122), new Size(1465, 180), "delete_reading_res");

            Label delete_reading_res_l = Create_Label(new Point(10, 10), "delete_reading_res_l", delete_reading_res);
            TextBox delete_reading_res_inp = Create_TextBox(new Point(10, 35), "delete_reading_res_inp", delete_reading_res);
            Label delete_reading_res_l2 = Create_Label(new Point(10, 68), "delete_reading_res_l2", delete_reading_res);
            TextBox delete_reading_res_inp2 = Create_TextBox(new Point(10, 93), "delete_reading_res_inp2", delete_reading_res);
            Button delete_reading_res_go = Create_button("Delete", delete_reading_res, 0, new EventHandler(delete_reading_res0_Click), new Point(732, 126));
        }
        public void delete_dropped_res() //Panel for input of what to delete from dropped
        {
            Panel delete_dropped_res = Create_Panel(new Point(10, 122), new Size(1465, 122), "delete_dropped_res");

            Label delete_dropped_res_l = Create_Label(new Point(10, 10), "delete_dropped_res_l", delete_dropped_res);
            TextBox delete_dropped_res_inp = Create_TextBox(new Point(10, 35), "delete_dropped_res_inp", delete_dropped_res);
            Button delete_dropped_res_go = Create_button("Delete", delete_dropped_res, 0, new EventHandler(delete_dropped_res0_Click), new Point(732, 68));
        }
        public void delete_tags_res() //Panel for input of what to delete from tags
        {
            Panel delete_tags_res = Create_Panel(new Point(10, 122), new Size(1465, 122), "delete_tags_res");

            Label delete_tags_res_l = Create_Label(new Point(10, 10), "delete_tags_res_l", delete_tags_res);
            TextBox delete_tags_res_inp = Create_TextBox(new Point(10, 35), "delete_tags_res_inp", delete_tags_res);
            Button delete_tags_res_go = Create_button("Delete", delete_tags_res, 0, new EventHandler(delete_tags_res0_Click), new Point(732, 68));
        }
        public void delete_book_tags_res() //Panel for input of what to delete from book_tags
        {
            Panel delete_book_tags_res = Create_Panel(new Point(10, 122), new Size(1465, 180), "delete_book_tags_res");

            Label delete_book_tags_res_l = Create_Label(new Point(10, 10), "delete_book_tags_res_l", delete_book_tags_res);
            TextBox delete_book_tags_res_inp = Create_TextBox(new Point(10, 35), "delete_book_tags_res_inp", delete_book_tags_res);
            Label delete_book_tags_res_l2 = Create_Label(new Point(10, 68), "delete_book_tags_res_l2", delete_book_tags_res);
            TextBox delete_book_tags_res_inp2 = Create_TextBox(new Point(10, 93), "delete_book_tags_res_inp2", delete_book_tags_res);
            Button delete_book_tags_res_go = Create_button("Delete", delete_book_tags_res, 0, new EventHandler(delete_book_tags_res0_Click), new Point(732, 126));
        }

        public void login_inp() //Panel for logging in 
        {
            Panel login_inp = Create_Panel(new Point(10, 66), new Size(1465, 180), "login_inp");

            Label login_name_l = Create_Label(new Point(10, 10), "login_name_l", login_inp);
            TextBox login_name_inp = Create_TextBox(new Point(10, 35), "login_name_inp", login_inp);
            Label login_pass_l = Create_Label(new Point(10, 68), "login_pass_l", login_inp);
            TextBox login_pass_inp = Create_TextBox(new Point(10, 93), "login_pass_inp", login_inp);
            Button login_in = Create_button("Log in", login_inp, 0, new EventHandler(login_inp0_Click), new Point(732, 126));
        }

        public void logged_as() //showing who's logged in
        {
            Panel logged_as = Create_Panel(new Point(10, 650), new Size(200, 35), "logged_as");

            Label logged_as_l = Create_Label(new Point(10, 10), "logged_as_l", logged_as);
            logged_as_l.Text = "";
        }

        public void backup() //panel for backups
        {
            Panel backup = Create_Panel(new Point(10, 66), new Size(1465, 46), "backup");

            Button backup_b = Create_button("Backup DB", backup, 0, new EventHandler(backup0_Click));
            Button backup_r = Create_button("Rollback DB", backup, 1, new EventHandler(backup1_Click));
        }

        public Button Create_button(string text, Panel p, int column, EventHandler e) //creating button
        {
            Button mybutton = new Button();
            mybutton.Location = new Point(column * 209, 0);
            mybutton.Height = 44;
            mybutton.Width = 205;
            mybutton.BackColor = Color.Gainsboro;
            mybutton.ForeColor = Color.Black;
            mybutton.Text = text;
            mybutton.Name = p.Name.ToString() + column.ToString();
            mybutton.AccessibleName = p.Name.ToString() + column.ToString();
            mybutton.Click += e;
            mybutton.Font = new Font("Georgia", 12);
            p.Controls.Add(mybutton);
            return mybutton;
        }

        public Button Create_button(string text, Panel pan, int column, EventHandler e, Point p) //creating button at point p
        {
            Button mybutton = new Button();
            mybutton.Location = p;
            mybutton.Height = 44;
            mybutton.Width = 205;
            mybutton.BackColor = Color.Gainsboro;
            mybutton.ForeColor = Color.Black;
            mybutton.Text = text;
            mybutton.Name = pan.Name.ToString() + column.ToString();
            mybutton.AccessibleName = pan.Name.ToString() + column.ToString();
            mybutton.Click += e;
            mybutton.Font = new Font("Georgia", 12);
            pan.Controls.Add(mybutton);
            return mybutton;
        }

        public Panel Create_Panel(Point p, Size s, string name) //creating panel to hold stuff
        {
            Panel pan = new Panel();
            pan.Location = p;
            pan.Size = s;
            pan.BorderStyle = BorderStyle.Fixed3D;
            pan.Name = name;

            this.Controls.Add(pan);
            pan.Visible = false;

            return pan;
        }

        public TextBox Create_TextBox(Point p, string name, Panel pan) //creating textbox for input
        {
            TextBox t = new TextBox();
            t.Location = p;
            t.Size = new Size(1445, 23);
            t.Name = name;
            pan.Controls.Add(t);

            return t;
        }

        public DataGridView Create_DataGridView(Point p, Size s, string name, Panel pan) //creating datagridviewer for output
        {
            DataGridView data = new DataGridView();
            data.Location = p;
            data.Size = s;
            data.Name = name;
            pan.Controls.Add(data);

            return data;
        }

        public Label Create_Label(Point p, string name, Panel pan) //creating label to communicate with user
        {
            Label l = new Label();
            l.Location = p;
            l.Name = name;
            l.Size = new Size(1445, 23);
            l.Font = new Font("Georgia", 12);
            pan.Controls.Add(l);

            return l;
        }

        public void HideAll_ShowOne(string name) //hide all panels but show one of them
        {
            this.Controls["show_tables"].Visible = false; //buttons to show books in table
            this.Controls["show_res"].Visible = false; //reasults of show
            this.Controls["search_by"].Visible = false; //buttons to serach by
            this.Controls["search_by_tags_tables"].Visible = false; //buttons to search by tags in table
            this.Controls["search_by_tags_res"].Visible = false; //textbox to input tags and results
            this.Controls["search_by_name_tables"].Visible = false; //buttons to search by name in table
            this.Controls["search_by_name_res"].Visible = false; //textbox to input tags and results
            this.Controls["insert_tables"].Visible = false; //buttons to choose which table we're inserting into
            this.Controls["insert_books_inp"].Visible = false; //typing in values to insert into books
            this.Controls["insert_planned_inp"].Visible = false; //typing in values to insert into planned
            this.Controls["insert_reading_inp"].Visible = false; //typing in values to insert into reading
            this.Controls["insert_dropped_inp"].Visible = false; //typing in values to insert into dropped
            this.Controls["insert_tags_inp"].Visible = false; //typing in values to insert into tags
            this.Controls["insert_book_tags_inp"].Visible = false; //typing in values to insert into book_tags
            this.Controls["update_tables"].Visible = false; //buttons to update table
            this.Controls["update_books_val"].Visible = false; //buttons to choose which value to update in books
            this.Controls["update_planned_val"].Visible = false; //buttons to choose which value to update in planned
            this.Controls["update_reading_val"].Visible = false; //buttons to choose which value to update in reading
            this.Controls["update_dropped_val"].Visible = false; //buttons to choose which value to update in dropped
            this.Controls["update_tags_val"].Visible = false; //buttons to choose which value to update in tags
            this.Controls["update_book_tags_val"].Visible = false; //buttons to choose which value to update in book_tags
            this.Controls["update_res"].Visible = false; //inputs to change values when updating
            this.Controls["delete_tables"].Visible = false; //buttons to choose which table we're deleting from
            this.Controls["delete_books_res"].Visible = false; //delete from books
            this.Controls["delete_planned_res"].Visible = false; //delete from planned
            this.Controls["delete_reading_res"].Visible = false; //delete from reading
            this.Controls["delete_dropped_res"].Visible = false; //delete from dropped
            this.Controls["delete_tags_res"].Visible = false; //delete from tags
            this.Controls["delete_book_tags_res"].Visible = false; //delete from book_tags
            this.Controls["login_inp"].Visible = false; //logging in
            this.Controls["backup"].Visible = false; //logging in            

            this.Controls[name].Visible = true; 
        }

        public string SearchString(string from, string by, string[] a) //select for search
        {
            string search = "Select * from " + from + " where " + by + " in (\""; //make a string we'll use to check where clause
            for(int i = 0; i < a.Length; i++)
            {
                search += a[i];

                if (i == a.Length - 1)
                {
                    search += "\")";
                }
                else
                {
                    search += "\",\"";
                }

                if(a[i].Contains("\"") == true) //sql inject
                {
                    return null;
                }
                else if(a[i].Contains("--") == true)
                {
                    return null;
                }
            }

            return search;
        }

        public void P_InsertBooks(string n, string a, int c, int p, double r, string l) //prepared statement insert into books
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Insert into books (name, author, chapters, pages, rating, link) values (@n, @a, @c, @p, @r, @l)";
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Parameters.AddWithValue("@a", a);
                cmd.Parameters.AddWithValue("@c", c);
                cmd.Parameters.AddWithValue("@p", p);
                cmd.Parameters.AddWithValue("@r", r);
                cmd.Parameters.AddWithValue("@l", l);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Inserted a book into database successfully.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void P_InsertPlanned(string n, int i) //prepared statement insert into planned
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;

            try
            {
                cmd.CommandText = "Select count(*) from planned";
                int res = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Insert into planned (name, interest) values (@name, @count)"; //insert new book
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Parameters.AddWithValue("@count", res + 1);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Call change_interest(@n, @target)"; //change its interest
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Parameters.AddWithValue("@target", i);
                cmd.CommandTimeout = 5;
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Changed book's interest successfully.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                MessageBox.Show("An exception of type " + e.GetType() +
                " was encountered while inserting the data. \nBook's interest wasn't changed.");
                MessageBox.Show(e.ToString());
            }
        }

        public void P_InsertReading(string n, int p) //prepared statement insert into reading
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;

            try //remove from planned
            {
                cmd.CommandText = "Select count(*) from planned";
                int res = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Call change_interest(@name, @target)"; //change book interest to last place
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Parameters.AddWithValue("@target", res);
                cmd.Prepare();
                cmd.ExecuteNonQuery();


                cmd.CommandText = "select id_planned from planned where name = @n limit 1"; //get book id
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Prepare();
                int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "delete from planned where id_planned = @id"; //delete book from planned
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "Insert into reading (name, progress) values (@na, @p)"; //insert into reading
                cmd.Parameters.AddWithValue("@na", n);
                cmd.Parameters.AddWithValue("@p", p);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Moved a book from planned list to reading list successfully.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                MessageBox.Show("An exception of type " + e.GetType() +
                " was encountered while inserting the data. \nEverything has been rolled back.");
                MessageBox.Show(e.ToString());
            }
        }

        public void P_InsertDropped(string n, string r) //prepared statement insert into dropped
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;

            try
            {
                cmd.CommandText = "select id_reading from reading where name = @name limit 1"; //get book id
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Prepare();
                int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "select progress from reading where name = @nam limit 1"; //get progress
                cmd.Parameters.AddWithValue("@nam", n);
                cmd.Prepare();
                int prog = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Insert into dropped (name, progress, reason) values (@n, @prog, @r)"; //insert into dropped
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Parameters.AddWithValue("@prog", prog);
                cmd.Parameters.AddWithValue("@r", r);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Inserted a book from reading list into dropped list successfully.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                MessageBox.Show("An exception of type " + e.GetType() +
                " was encountered while inserting the data. \nEverything has been rolled back.");
                MessageBox.Show(e.ToString());
            }
        }

        public void P_InsertTags(string n) //prepared statement insert into tags
        {
            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.CommandTimeout = 5;

            try
            {
                cmd.CommandText = "Insert into tags (name) values (@n)";
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Inserted a new tag successfully.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void P_InsertBook_Tags(string n, string t) //prepared statement insert into book_tags
        {
            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.CommandTimeout = 5;

            try
            {
                cmd.CommandText = "Insert into book_tags (fk_book_id, fk_tag_id) values (@n, @t)";
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Parameters.AddWithValue("@t", t);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Added a new tag to a book successfully.");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void interaction0_Click(object sender, EventArgs e) // show all books in table
        {
            HideAll_ShowOne("show_tables");
        }

        private void show_tables0_Click(object sender, EventArgs e) //show from books
        {
            this.Controls["show_res"].Visible = true;

            string sql = "SELECT * FROM books";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "books");
            show_text.DataSource = ds.Tables["books"].DefaultView;
        }

        private void show_tables1_Click(object sender, EventArgs e) //show from books with tags
        {
            this.Controls["show_res"].Visible = true;

            string sql = "SELECT * FROM book_with_tags";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "book_with_tags");
            show_text.DataSource = ds.Tables["book_with_tags"].DefaultView;
        }

        private void show_tables2_Click(object sender, EventArgs e) //show from planned
        {
            this.Controls["show_res"].Visible = true;

            string sql = "SELECT * FROM planned";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "planned");
            show_text.DataSource = ds.Tables["planned"].DefaultView;
        }

        private void show_tables3_Click(object sender, EventArgs e) //show from planned with tags
        {
            this.Controls["show_res"].Visible = true;

            string sql = "SELECT id_planned, name, interest, tag FROM planned_info_tags";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "planned");
            show_text.DataSource = ds.Tables["planned"].DefaultView;
        }

        private void show_tables4_Click(object sender, EventArgs e) //show from reading
        {
            this.Controls["show_res"].Visible = true;

            string sql = "SELECT * FROM reading";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "reading");
            show_text.DataSource = ds.Tables["reading"].DefaultView;
        }

        private void show_tables5_Click(object sender, EventArgs e) //show from dropped
        {
            this.Controls["show_res"].Visible = true;

            string sql = "SELECT * FROM dropped";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "dropped");
            show_text.DataSource = ds.Tables["dropped"].DefaultView;
        }

        private void show_tables6_Click(object sender, EventArgs e) //show from tags
        {
            this.Controls["show_res"].Visible = true;

            string sql = "SELECT * FROM tags ORDER BY id_tags";
            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "tags");
            show_text.DataSource = ds.Tables["tags"].DefaultView;
        }

        private void interaction1_Click(object sender, EventArgs e) //search by
        {
            HideAll_ShowOne("search_by");
        }

        private void search_by0_Click(object sender, EventArgs e) //search by name
        {
            this.Controls["search_by_tags_tables"].Visible = false;
            this.Controls["search_by_tags_res"].Visible = false;
            this.Controls["search_by_name_tables"].Visible = true;
            from = "";
        }

        private void search_by_name_tables0_Click(object sender, EventArgs e) //search by name in books
        {
            this.Controls["search_by_name_res"].Visible = true;
            this.Controls["search_by_name_res"].Controls["search_by_name_l"].Text = "Input names to lookup in books database separated by semicolon in the text box below.";
            this.Controls["search_by_name_res"].Controls["search_by_name_inp"].Text = "";
            from = "books";
        }

        private void search_by_name_tables1_Click(object sender, EventArgs e) //search by name in planned
        {
            this.Controls["search_by_name_res"].Visible = true;
            this.Controls["search_by_name_res"].Controls["search_by_name_l"].Text = "Input names to lookup in planned list separated by semicolon in the text box below.";
            this.Controls["search_by_name_res"].Controls["search_by_name_inp"].Text = "";
            from = "planned";
        }

        private void search_by_name_tables2_Click(object sender, EventArgs e) //search by name in reading
        {
            this.Controls["search_by_name_res"].Visible = true;
            this.Controls["search_by_name_res"].Controls["search_by_name_l"].Text = "Input names to lookup in reading list separated by semicolon in the text box below.";
            this.Controls["search_by_name_res"].Controls["search_by_name_inp"].Text = "";
            from = "reading";
        }

        private void search_by_name_tables3_Click(object sender, EventArgs e) //search by name in dropped
        {
            this.Controls["search_by_name_res"].Visible = true;
            this.Controls["search_by_name_res"].Controls["search_by_name_l"].Text = "Input names to lookup in dropped list separated by semicolon in the text box below.";
            this.Controls["search_by_name_res"].Controls["search_by_name_inp"].Text = "";
            from = "dropped";
        }        

        private void search_by_name_res0_Click(object sender, EventArgs e) // search by name input
        {
            try
            {
                string[] names = this.Controls["search_by_name_res"].Controls["search_by_name_inp"].Text.Split(';');
                string sql = SearchString(from, "name", names);

                if(sql != null)
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, from);
                    search_by_name_text.DataSource = ds.Tables[from].DefaultView;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void search_by1_Click(object sender, EventArgs e) //search by tags
        {
            this.Controls["search_by_tags_tables"].Visible = true;
            this.Controls["search_by_name_tables"].Visible = false;
            this.Controls["search_by_name_res"].Visible = false;
            from = "";
        }

        private void search_by_tags_tables0_Click(object sender, EventArgs e) //search by tags in books
        {
            this.Controls["search_by_tags_res"].Visible = true;
            search_by_tags_text.Visible = false;
            this.Controls["search_by_tags_res"].Controls["search_by_tags_l"].Text = "Input tag names to lookup in books database separated by semicolon in the text box below.";
            this.Controls["search_by_tags_res"].Controls["search_by_tags_inp"].Text = "";
            from = "book_with_tags";
        }

        private void search_by_tags_tables1_Click(object sender, EventArgs e) //search by tags in planned
        {
            this.Controls["search_by_tags_res"].Visible = true;
            search_by_tags_text.Visible = false;
            this.Controls["search_by_tags_res"].Controls["search_by_tags_l"].Text = "Input tag names to lookup in planned list separated by semicolon in the text box below.";
            this.Controls["search_by_tags_res"].Controls["search_by_tags_inp"].Text = "";
            from = "planned_info_tags";
        }

        private void search_by_tags_res0_Click(object sender, EventArgs e) //search by tags input
        {
            try
            {
                search_by_tags_text.Visible = true;
                string[] tags = this.Controls["search_by_tags_res"].Controls["search_by_tags_inp"].Text.Split(';');
                string sql = SearchString(from, "tag", tags);

                if (sql != null)
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, from);
                    search_by_tags_text.DataSource = ds.Tables[from].DefaultView;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void interaction2_Click(object sender, EventArgs e) //insert
        {
            HideAll_ShowOne("insert_tables");
        }

        private void insert_tables0_Click(object sender, EventArgs e) //insert into books
        {
            this.Controls["insert_books_inp"].Visible = true;
            this.Controls["insert_planned_inp"].Visible = false;
            this.Controls["insert_reading_inp"].Visible = false;
            this.Controls["insert_dropped_inp"].Visible = false;
            this.Controls["insert_tags_inp"].Visible = false;
            this.Controls["insert_book_tags_inp"].Visible = false;
            this.Controls["insert_books_inp"].Controls["insert_books_name_l"].Text = "Input name in the text box below.";
            this.Controls["insert_books_inp"].Controls["insert_books_name_text"].Text = "";
            this.Controls["insert_books_inp"].Controls["insert_books_author_l"].Text = "Input author in the text box below.";
            this.Controls["insert_books_inp"].Controls["insert_books_author_text"].Text = "";
            this.Controls["insert_books_inp"].Controls["insert_books_chapters_l"].Text = "Input chapters in the text box below.";
            this.Controls["insert_books_inp"].Controls["insert_books_chapters_text"].Text = "";
            this.Controls["insert_books_inp"].Controls["insert_books_pages_l"].Text = "Input pages in the text box below.";
            this.Controls["insert_books_inp"].Controls["insert_books_pages_text"].Text = "";
            this.Controls["insert_books_inp"].Controls["insert_books_rating_l"].Text = "Input rating in the text box below.";
            this.Controls["insert_books_inp"].Controls["insert_books_rating_text"].Text = "";
            this.Controls["insert_books_inp"].Controls["insert_books_link_l"].Text = "Input link  in the text box below.";
            this.Controls["insert_books_inp"].Controls["insert_books_link_text"].Text = "";
        }

        private void insert_books_inp0_Click(object sender, EventArgs e) //pressed insert
        {
            string n = this.Controls["insert_books_inp"].Controls["insert_books_name_text"].Text;
            string a = this.Controls["insert_books_inp"].Controls["insert_books_author_text"].Text;
            int c = 1;
            try
            {
                c = Int32.Parse(this.Controls["insert_books_inp"].Controls["insert_books_chapters_text"].Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Chapter number must be an integer.");
            }
            int p = 0;
            try
            {
                p = Int32.Parse(this.Controls["insert_books_inp"].Controls["insert_books_pages_text"].Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Page number must be an integer.");
            }
            double r = 0.0;
            try
            {
                r = double.Parse(this.Controls["insert_books_inp"].Controls["insert_books_rating_text"].Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Page number must be an integer.");
            }

            string l = this.Controls["insert_books_inp"].Controls["insert_books_link_text"].Text;

            try
            {
                P_InsertBooks(n, a, c, p, r, l);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void insert_tables1_Click(object sender, EventArgs e) //insert into planned from books
        {
            this.Controls["insert_books_inp"].Visible = false;
            this.Controls["insert_planned_inp"].Visible = true;
            this.Controls["insert_reading_inp"].Visible = false;
            this.Controls["insert_dropped_inp"].Visible = false;
            this.Controls["insert_tags_inp"].Visible = false;
            this.Controls["insert_book_tags_inp"].Visible = false;
            this.Controls["insert_planned_inp"].Controls["insert_planned_name_l"].Text = "Input name in the text box below.";
            this.Controls["insert_planned_inp"].Controls["insert_planned_name_text"].Text = "";
            this.Controls["insert_planned_inp"].Controls["insert_planned_interest_l"].Text = "Input interest in the text box below.";
            this.Controls["insert_planned_inp"].Controls["insert_planned_interest_text"].Text = "";
        }

        private void insert_planned_inp0_Click(object sender, EventArgs e) //pressed insert
        {
            string n = this.Controls["insert_planned_inp"].Controls["insert_planned_name_text"].Text;
            int i = 0;
            try
            {
                i = Int32.Parse(this.Controls["insert_planned_inp"].Controls["insert_planned_interest_text"].Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Interest number must be an integer between 1 and the number of books in planned list.");
            }
            
            try
            {
                P_InsertPlanned(n, i);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void insert_tables2_Click(object sender, EventArgs e) //insert into reading from planned
        {
            this.Controls["insert_books_inp"].Visible = false;
            this.Controls["insert_planned_inp"].Visible = false;
            this.Controls["insert_reading_inp"].Visible = true;
            this.Controls["insert_dropped_inp"].Visible = false;
            this.Controls["insert_tags_inp"].Visible = false;
            this.Controls["insert_book_tags_inp"].Visible = false;
            this.Controls["insert_reading_inp"].Visible = true;
            this.Controls["insert_reading_inp"].Controls["insert_reading_name_l"].Text = "Input name in the text box below.";
            this.Controls["insert_reading_inp"].Controls["insert_reading_name_text"].Text = "";
            this.Controls["insert_reading_inp"].Controls["insert_reading_progress_l"].Text = "Input progress in the text box below.";
            this.Controls["insert_reading_inp"].Controls["insert_reading_progress_text"].Text = "";
        }

        private void insert_reading_inp0_Click(object sender, EventArgs e) //pressed insert
        {
            string n = this.Controls["insert_reading_inp"].Controls["insert_reading_name_text"].Text;
            int p = 0;
            try
            {
                p = Int32.Parse(this.Controls["insert_reading_inp"].Controls["insert_reading_progress_text"].Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Progress must be an integer between 1 and the number of chapters of the book.");
            }

            try
            {
                P_InsertReading(n, p);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void insert_tables3_Click(object sender, EventArgs e) //insert into dropped from reading
        {
            this.Controls["insert_books_inp"].Visible = false;
            this.Controls["insert_planned_inp"].Visible = false;
            this.Controls["insert_reading_inp"].Visible = false;
            this.Controls["insert_dropped_inp"].Visible = true;
            this.Controls["insert_tags_inp"].Visible = false;
            this.Controls["insert_book_tags_inp"].Visible = false;
            this.Controls["insert_dropped_inp"].Visible = true;
            this.Controls["insert_dropped_inp"].Controls["insert_dropped_name_l"].Text = "Input name in the text box below.";
            this.Controls["insert_dropped_inp"].Controls["insert_dropped_name_text"].Text = "";
            this.Controls["insert_dropped_inp"].Controls["insert_dropped_reason_l"].Text = "Input reason in the text box below.";
            this.Controls["insert_dropped_inp"].Controls["insert_dropped_reason_text"].Text = "";
        }

        private void insert_dropped_inp0_Click(object sender, EventArgs e) //pressed insert
        {
            string n = this.Controls["insert_dropped_inp"].Controls["insert_dropped_name_text"].Text;
            string r = this.Controls["insert_dropped_inp"].Controls["insert_dropped_reason_text"].Text;            

            try
            {
                P_InsertDropped(n, r);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void insert_tables4_Click(object sender, EventArgs e) //insert into tags
        {
            this.Controls["insert_books_inp"].Visible = false;
            this.Controls["insert_planned_inp"].Visible = false;
            this.Controls["insert_reading_inp"].Visible = false;
            this.Controls["insert_dropped_inp"].Visible = false;
            this.Controls["insert_tags_inp"].Visible = true;
            this.Controls["insert_book_tags_inp"].Visible = false;
            this.Controls["insert_tags_inp"].Controls["insert_tags_name_l"].Text = "Input name in the text box below.";
            this.Controls["insert_tags_inp"].Controls["insert_tags_name_text"].Text = "";
        }

        private void insert_tags_inp0_Click(object sender, EventArgs e) //pressed insert
        {
            string n = this.Controls["insert_tags_inp"].Controls["insert_tags_name_text"].Text;

            try
            {
                P_InsertTags(n);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void insert_tables5_Click(object sender, EventArgs e) //insert into book_tags
        {
            this.Controls["insert_books_inp"].Visible = false;
            this.Controls["insert_planned_inp"].Visible = false;
            this.Controls["insert_reading_inp"].Visible = false;
            this.Controls["insert_dropped_inp"].Visible = false;
            this.Controls["insert_tags_inp"].Visible = false;
            this.Controls["insert_book_tags_inp"].Visible = true;
            this.Controls["insert_book_tags_inp"].Controls["insert_book_tags_name_l"].Text = "Input name in the text box below.";
            this.Controls["insert_book_tags_inp"].Controls["insert_book_tags_name_text"].Text = "";
            this.Controls["insert_book_tags_inp"].Controls["insert_book_tags_tag_l"].Text = "Input tag in the text box below.";
            this.Controls["insert_book_tags_inp"].Controls["insert_book_tags_tag_text"].Text = "";
        }

        private void insert_book_tags_inp0_Click(object sender, EventArgs e) //pressed insert
        {
            string n = this.Controls["insert_book_tags_inp"].Controls["insert_book_tags_name_text"].Text;
            string t = this.Controls["insert_book_tags_inp"].Controls["insert_book_tags_tag_text"].Text;

            try
            {
                P_InsertBook_Tags(n, t);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void interaction3_Click(object sender, EventArgs e) //update
        {
            HideAll_ShowOne("update_tables");
        }

        private void update_tables0_Click(object sender, EventArgs e) //update books
        {
            this.Controls["update_books_val"].Visible = true;
            this.Controls["update_planned_val"].Visible = false;
            this.Controls["update_reading_val"].Visible = false;
            this.Controls["update_dropped_val"].Visible = false;
            this.Controls["update_tags_val"].Visible = false;
            this.Controls["update_book_tags_val"].Visible = false;
            this.Controls["update_res"].Visible = false;

            from = "books";
        }

        private void update_books_val0_Click(object sender, EventArgs e) //changing name
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input old book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new book name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "name";
        }

        private void update_books_val1_Click(object sender, EventArgs e) //changing author
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new author name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "author";
        }

        private void update_books_val2_Click(object sender, EventArgs e) //changing chapters
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new number of chapters in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "chapters";
        }

        private void update_books_val3_Click(object sender, EventArgs e) //changing pages
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new number of pages in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "pages";
        }

        private void update_books_val4_Click(object sender, EventArgs e) //changing rating
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new rating in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "rating";
        }

        private void update_books_val5_Click(object sender, EventArgs e) //changing link
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new link to book in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "link";
        }

        private void update_tables1_Click(object sender, EventArgs e) //updating planned
        {
            this.Controls["update_books_val"].Visible = false;
            this.Controls["update_planned_val"].Visible = true;
            this.Controls["update_reading_val"].Visible = false;
            this.Controls["update_dropped_val"].Visible = false;
            this.Controls["update_tags_val"].Visible = false;
            this.Controls["update_book_tags_val"].Visible = false;
            this.Controls["update_res"].Visible = false;

            from = "planned";
        }

        private void update_planned_val0_Click(object sender, EventArgs e) //changing name
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input old book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new book name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "name";
        }

        private void update_planned_val1_Click(object sender, EventArgs e) //changing interest
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new interest in the book in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "interest";
        }

        private void update_tables2_Click(object sender, EventArgs e) //updating reading
        {
            this.Controls["update_books_val"].Visible = false;
            this.Controls["update_planned_val"].Visible = false;
            this.Controls["update_reading_val"].Visible = true;
            this.Controls["update_dropped_val"].Visible = false;
            this.Controls["update_tags_val"].Visible = false;
            this.Controls["update_book_tags_val"].Visible = false;
            this.Controls["update_res"].Visible = false;

            from = "reading";
        }

        private void update_reading_val0_Click(object sender, EventArgs e) //changing name
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input old book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new book name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "name";
        }

        private void update_reading_val1_Click(object sender, EventArgs e) //changing progress
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new reading progress in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "progress";
        }

        private void update_tables3_Click(object sender, EventArgs e) //updating dropped
        {
            this.Controls["update_books_val"].Visible = false;
            this.Controls["update_planned_val"].Visible = false;
            this.Controls["update_reading_val"].Visible = false;
            this.Controls["update_dropped_val"].Visible = true;
            this.Controls["update_tags_val"].Visible = false;
            this.Controls["update_book_tags_val"].Visible = false;
            this.Controls["update_res"].Visible = false;

            from = "dropped";
        }

        private void update_dropped_val0_Click(object sender, EventArgs e) //changing name
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input old book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new book name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "name";
        }

        private void update_dropped_val1_Click(object sender, EventArgs e) //changing progress
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new reading progress in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "progress";
        }

        private void update_dropped_val2_Click(object sender, EventArgs e) //changing reason
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new reason for dropping the book in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "reason";
        }

        private void update_tables4_Click(object sender, EventArgs e) //updating tags
        {
            this.Controls["update_books_val"].Visible = false;
            this.Controls["update_planned_val"].Visible = false;
            this.Controls["update_reading_val"].Visible = false;
            this.Controls["update_dropped_val"].Visible = false;
            this.Controls["update_tags_val"].Visible = true;
            this.Controls["update_book_tags_val"].Visible = false;
            this.Controls["update_res"].Visible = false;

            from = "tags";
        }

        private void update_tags_val0_Click(object sender, EventArgs e) //changing tag name
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input old tag name in the text box below.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new tag name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "name";
        }

        private void update_tables5_Click(object sender, EventArgs e) //updating book_tags
        {
            this.Controls["update_books_val"].Visible = false;
            this.Controls["update_planned_val"].Visible = false;
            this.Controls["update_reading_val"].Visible = false;
            this.Controls["update_dropped_val"].Visible = false;
            this.Controls["update_tags_val"].Visible = false;
            this.Controls["update_book_tags_val"].Visible = true;
            this.Controls["update_res"].Visible = false;

            from = "book_tags";
        }

        private void update_book_tags_val0_Click(object sender, EventArgs e) //changing tag to another book
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input old book name and the name of the tag you want to move in the text box below separated by semicolon.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new book name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "fk_book_id";
        }

        private void update_book_tags_val1_Click(object sender, EventArgs e) //changing tag of book
        {
            this.Controls["update_res"].Visible = true;
            this.Controls["update_res"].Controls["update_old_l"].Text = "Input book name and old tag name in the text box below separated by semicolon.";
            this.Controls["update_res"].Controls["update_old_inp"].Text = "";
            this.Controls["update_res"].Controls["update_new_l"].Text = "Input new tag name in the text box below.";
            this.Controls["update_res"].Controls["update_new_inp"].Text = "";

            what = "fk_tag_id";
        }

        private void update_res0_Click(object sender, EventArgs e) //pressed update
        {
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;
            string name = this.Controls["update_res"].Controls["update_old_inp"].Text;
            string value = this.Controls["update_res"].Controls["update_new_inp"].Text;

            try
            {
                switch (from) //options based on what we're changing
                {
                    case "books":
                        switch (what)
                        {
                            case "name":
                                cmd.CommandText = "Update books set name = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed book name of " + name + " to " + value);
                                break;

                            case "author":
                                cmd.CommandText = "Update books set author = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed author of " + name + " to " + value);
                                break;

                            case "chapters":
                                try
                                {
                                    cmd.CommandText = "Update books set chapters = @value where name = @name;";
                                    cmd.Parameters.AddWithValue("@value", Int32.Parse(value));
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    myTrans.Commit();
                                    MessageBox.Show("Changed chapters of " + name + " to " + value);
                                }
                                catch (FormatException form)
                                {
                                    MessageBox.Show(form.ToString());
                                }
                                break;

                            case "pages":
                                try
                                {
                                    cmd.CommandText = "Update books set pages = @value where name = @name;";
                                    cmd.Parameters.AddWithValue("@value", Int32.Parse(value));
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    myTrans.Commit();
                                    MessageBox.Show("Changed pages of " + name + " to " + value);
                                }
                                catch (FormatException form)
                                {
                                    MessageBox.Show(form.ToString());
                                }
                                break;

                            case "rating":
                                try
                                {
                                    cmd.CommandText = "Update books set rating = @value where name = @name;";
                                    cmd.Parameters.AddWithValue("@value", Double.Parse(value));
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    myTrans.Commit();
                                    MessageBox.Show("Changed rating of " + name + " to " + value);
                                }
                                catch (FormatException form)
                                {
                                    MessageBox.Show(form.ToString());
                                }
                                break;

                            case "link":
                                cmd.CommandText = "Update books set link = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed link of " + name + " to " + value);
                                break;

                            default:
                                break;
                        }
                        break;

                    case "planned":
                        switch (what)
                        {
                            case "name":
                                cmd.CommandText = "Update planned set name = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed book name of " + name + " to " + value);
                                break;

                            case "interest":
                                try
                                {
                                    cmd.CommandText = "Call change_interest(@name, @target)";
                                    cmd.Parameters.AddWithValue("@target", Int32.Parse(value));
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    myTrans.Commit();
                                    MessageBox.Show("Changed interest of " + name + " to " + value);
                                }
                                catch (FormatException form)
                                {
                                    MessageBox.Show(form.ToString());
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    case "reading":
                        switch (what)
                        {
                            case "name":
                                cmd.CommandText = "Update reading set name = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed book name of " + name + " to " + value);
                                break;

                            case "progress":
                                try
                                {
                                    cmd.CommandText = "Update reading set progress = @value where name = @name;";
                                    cmd.Parameters.AddWithValue("@value", Int32.Parse(value));
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    myTrans.Commit();
                                    MessageBox.Show("Changed progress of " + name + " to " + value);
                                }
                                catch (FormatException form)
                                {
                                    MessageBox.Show(form.ToString());
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    case "dropped":
                        switch (what)
                        {
                            case "name":
                                cmd.CommandText = "Update dropped set name = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed book name of " + name + " to " + value);
                                break;

                            case "progress":
                                try
                                {
                                    cmd.CommandText = "Update dropped set progress = @value where name = @name;";
                                    cmd.Parameters.AddWithValue("@value", Int32.Parse(value));
                                    cmd.Parameters.AddWithValue("@name", name);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    myTrans.Commit();
                                    MessageBox.Show("Changed progress of " + name + " to " + value);
                                }
                                catch (FormatException form)
                                {
                                    MessageBox.Show(form.ToString());
                                }
                                break;

                            case "reason":
                                cmd.CommandText = "Update dropped set reason = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed reason for dropping " + name + " to " + value);
                                break;

                            default:
                                break;
                        }
                        break;

                    case "tags":
                        switch (what)
                        {
                            case "name":
                                cmd.CommandText = "Update tags set name = @value where name = @name;";
                                cmd.Parameters.AddWithValue("@value", value);
                                cmd.Parameters.AddWithValue("@name", name);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                myTrans.Commit();
                                MessageBox.Show("Changed tag name of " + name + " to " + value);
                                break;

                            default:
                                break;
                        }
                        break;

                    case "book_tags":
                        switch (what)
                        {
                            case "fk_book_id":
                                try
                                {
                                    string[] inp = name.Split(';');

                                    cmd.CommandText = "select id_books from books where name = @name limit 1"; //get old book id
                                    cmd.Parameters.AddWithValue("@name", inp[0]);
                                    cmd.Prepare();
                                    int id_old = Int32.Parse(cmd.ExecuteScalar().ToString());

                                    cmd.CommandText = "select id_tags from tags where name = @nam limit 1"; //get tag id
                                    cmd.Parameters.AddWithValue("@nam", inp[1]);
                                    cmd.Prepare();
                                    int tag = Int32.Parse(cmd.ExecuteScalar().ToString());

                                    cmd.CommandText = "select id_books from books where name = @na limit 1"; //get new book id
                                    cmd.Parameters.AddWithValue("@na", value);
                                    cmd.Prepare();
                                    int id_new = Int32.Parse(cmd.ExecuteScalar().ToString());

                                    cmd.CommandText = "Update book_tags set fk_book_id = @value where fk_book_id = @book_id and fk_tag_id = @tag_id;"; //change book id
                                    cmd.Parameters.AddWithValue("@value", id_new);
                                    cmd.Parameters.AddWithValue("@book_id", id_old);
                                    cmd.Parameters.AddWithValue("@tag_id", tag);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();

                                    myTrans.Commit();
                                    MessageBox.Show("Changed tag " + inp[1] + " from book " + inp[0] + " to " + value);
                                }
                                catch (NullReferenceException nex)
                                {
                                    MessageBox.Show(nex.ToString());
                                }
                                break;

                            case "fk_tag_id":
                                try
                                {
                                    string[] inp = name.Split(';');

                                    cmd.CommandText = "select id_books from books where name = @name limit 1"; //get book id
                                    cmd.Parameters.AddWithValue("@name", inp[0]);
                                    cmd.Prepare();
                                    int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                                    cmd.CommandText = "select id_tags from tags where name = @nam limit 1"; //get old tag id
                                    cmd.Parameters.AddWithValue("@nam", inp[1]);
                                    cmd.Prepare();
                                    int tag_old = Int32.Parse(cmd.ExecuteScalar().ToString());

                                    cmd.CommandText = "select id_tags from tags where name = @na limit 1"; //get new tag id
                                    cmd.Parameters.AddWithValue("@na", value);
                                    cmd.Prepare();
                                    int tag_new = Int32.Parse(cmd.ExecuteScalar().ToString());

                                    cmd.CommandText = "Update book_tags set fk_tag_id = @value where fk_book_id = @book_id and fk_tag_id = @tag_id;"; //change tag id
                                    cmd.Parameters.AddWithValue("@value", tag_new);
                                    cmd.Parameters.AddWithValue("@book_id", id);
                                    cmd.Parameters.AddWithValue("@tag_id", tag_old);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();

                                    myTrans.Commit();
                                    MessageBox.Show("Changed tag " + inp[1] + " to " + value + " for book " + inp[0]);
                                }
                                catch (NullReferenceException nex)
                                {
                                    MessageBox.Show(nex.ToString());
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (MySqlException ex)
            {
                try
                {
                    myTrans.Rollback();
                    MessageBox.Show("Something went wrong, rolling back changes.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Something went wrong during rollback: " + exc.ToString());
                }

                MessageBox.Show(ex.ToString());
            }
        }

        private void interaction4_Click(object sender, EventArgs e) //delete
        {
            HideAll_ShowOne("delete_tables");
        }

        private void delete_tables0_Click(object sender, EventArgs e) //delete from books
        {
            this.Controls["delete_books_res"].Visible = true;
            this.Controls["delete_planned_res"].Visible = false;
            this.Controls["delete_reading_res"].Visible = false;
            this.Controls["delete_dropped_res"].Visible = false;
            this.Controls["delete_tags_res"].Visible = false;
            this.Controls["delete_book_tags_res"].Visible = false;
            this.Controls["delete_books_res"].Controls["delete_books_res_l"].Text = "Input book name to delete from whole database - must be a perfect match.";
            this.Controls["delete_books_res"].Controls["delete_books_res_inp"].Text = "";
        }

        private void delete_books_res0_Click(object sender, EventArgs e) //pressed delete
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;
            string n = this.Controls["delete_books_res"].Controls["delete_books_res_inp"].Text;

            try
            {
                cmd.CommandText = "Select count(*) from planned";
                int res = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Call change_interest(@name, @target)"; //change book interest to last place
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Parameters.AddWithValue("@target", res);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select id_books from books where name = @nam limit 1"; //get book id
                cmd.Parameters.AddWithValue("@nam", n);
                cmd.Prepare();
                int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Delete from books where id_books = @id"; //delete from books
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Deleted book from the database successfully.");
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                MessageBox.Show("An exception of type " + ex.GetType() +
                " was encountered while inserting the data. \nEverything has been rolled back.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void delete_tables1_Click(object sender, EventArgs e) //delete from planned
        {
            this.Controls["delete_books_res"].Visible = false;
            this.Controls["delete_planned_res"].Visible = true;
            this.Controls["delete_reading_res"].Visible = false;
            this.Controls["delete_dropped_res"].Visible = false;
            this.Controls["delete_tags_res"].Visible = false;
            this.Controls["delete_book_tags_res"].Visible = false;
            this.Controls["delete_planned_res"].Controls["delete_planned_res_l"].Text = "Input book name to delete from planning list - must be a perfect match.";
            this.Controls["delete_planned_res"].Controls["delete_planned_res_inp"].Text = "";
        }

        private void delete_planned_res0_Click(object sender, EventArgs e) //pressed delete
        {
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;
            string n = this.Controls["delete_planned_res"].Controls["delete_planned_res_inp"].Text;

            try
            {
                cmd.CommandText = "Select count(*) from planned";
                int res = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Call change_interest(@name, @target)"; //change book interest to last place
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Parameters.AddWithValue("@target", res);
                cmd.Prepare();
                cmd.ExecuteNonQuery();


                cmd.CommandText = "select id_planned from planned where name = @n limit 1"; //get book id
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Prepare();
                int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "delete from planned where id_planned = @id"; //delete book from planned
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Deleted book from planned list successfully.");
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException exc)
                {
                    MessageBox.Show(exc.ToString());
                }
                MessageBox.Show("An exception of type " + ex.GetType() +
                " was encountered while inserting the data.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void delete_tables2_Click(object sender, EventArgs e) //delete from reading, add to dropped
        {
            this.Controls["delete_books_res"].Visible = false;
            this.Controls["delete_planned_res"].Visible = false;
            this.Controls["delete_reading_res"].Visible = true;
            this.Controls["delete_dropped_res"].Visible = false;
            this.Controls["delete_tags_res"].Visible = false;
            this.Controls["delete_book_tags_res"].Visible = false;
            this.Controls["delete_reading_res"].Controls["delete_reading_res_l"].Text = "Input book name to move from reading list to dropped list - must be a perfect match.";
            this.Controls["delete_reading_res"].Controls["delete_reading_res_inp"].Text = "";
            this.Controls["delete_reading_res"].Controls["delete_reading_res_l2"].Text = "Input reason for moving to dropped list";
            this.Controls["delete_reading_res"].Controls["delete_reading_res_inp2"].Text = "";
        }

        private void delete_reading_res0_Click(object sender, EventArgs e) //pressed delete
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;
            string n = this.Controls["delete_reading_res"].Controls["delete_reading_res_inp"].Text;
            string r = this.Controls["delete_reading_res"].Controls["delete_reading_res_inp2"].Text;

            try
            {
                cmd.CommandText = "select id_reading from reading where name = @name limit 1"; //get book id
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Prepare();
                int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "select progress from reading where name = @nam limit 1"; //get progress
                cmd.Parameters.AddWithValue("@nam", n);
                cmd.Prepare();
                int prog = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Insert into dropped (name, progress, reason) values (@n, @prog, @r)"; //insert into dropped
                cmd.Parameters.AddWithValue("@n", n);
                cmd.Parameters.AddWithValue("@prog", prog);
                cmd.Parameters.AddWithValue("@r", r);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Inserted a book from reading list into dropped list successfully.");
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                MessageBox.Show("An exception of type " + ex.GetType() +
                " was encountered while inserting the data. \nEverything has been rolled back.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void delete_tables3_Click(object sender, EventArgs e) //delete from dropped
        {
            this.Controls["delete_books_res"].Visible = false;
            this.Controls["delete_planned_res"].Visible = false;
            this.Controls["delete_reading_res"].Visible = false;
            this.Controls["delete_dropped_res"].Visible = true;
            this.Controls["delete_tags_res"].Visible = false;
            this.Controls["delete_book_tags_res"].Visible = false;
            this.Controls["delete_dropped_res"].Controls["delete_dropped_res_l"].Text = "Input book name to delete from dropped list - must be a perfect match.";
            this.Controls["delete_dropped_res"].Controls["delete_dropped_res_inp"].Text = "";
        }

        private void delete_dropped_res0_Click(object sender, EventArgs e) //pressed delete
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;
            string n = this.Controls["delete_dropped_res"].Controls["delete_dropped_res_inp"].Text;

            try
            {
                cmd.CommandText = "select id_dropped from dropped where name = @name limit 1"; //get book id
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Prepare();
                int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Delete from dropped where id_dropped = @id"; //delete from dropped
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Deleted book from planned list successfully.");
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                MessageBox.Show("An exception of type " + ex.GetType() +
                " was encountered while inserting the data. \nEverything has been rolled back.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void delete_tables4_Click(object sender, EventArgs e) //delete from tags
        {
            this.Controls["delete_books_res"].Visible = false;
            this.Controls["delete_planned_res"].Visible = false;
            this.Controls["delete_reading_res"].Visible = false;
            this.Controls["delete_dropped_res"].Visible = false;
            this.Controls["delete_tags_res"].Visible = true;
            this.Controls["delete_book_tags_res"].Visible = false;
            this.Controls["delete_tags_res"].Controls["delete_tags_res_l"].Text = "Input tag name to delete from tags list - must be a perfect match.";
            this.Controls["delete_tags_res"].Controls["delete_tags_res_inp"].Text = "";
        }

        private void delete_tags_res0_Click(object sender, EventArgs e) //pressed delete
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;
            string n = this.Controls["delete_tags_res"].Controls["delete_tags_res_inp"].Text;

            try
            {
                cmd.CommandText = "select id_tags from tags where name = @name limit 1"; //get book id
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Prepare();
                int id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Delete from tags where id_tags = @id"; //delete from tags
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Deleted book from tags list successfully.");
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                MessageBox.Show("An exception of type " + ex.GetType() +
                " was encountered while inserting the data. \nEverything has been rolled back.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void delete_tables5_Click(object sender, EventArgs e) //delete book tags
        {
            this.Controls["delete_books_res"].Visible = false;
            this.Controls["delete_planned_res"].Visible = false;
            this.Controls["delete_reading_res"].Visible = false;
            this.Controls["delete_dropped_res"].Visible = false;
            this.Controls["delete_tags_res"].Visible = false;
            this.Controls["delete_book_tags_res"].Visible = true;
            this.Controls["delete_book_tags_res"].Controls["delete_book_tags_res_l"].Text = "Input book name to delete a tag from - must be a perfect match.";
            this.Controls["delete_book_tags_res"].Controls["delete_book_tags_res_inp"].Text = "";
            this.Controls["delete_book_tags_res"].Controls["delete_book_tags_res_l2"].Text = "Input tag name to delete.";
            this.Controls["delete_book_tags_res"].Controls["delete_book_tags_res_inp"].Text = "";
        }

        private void delete_book_tags_res0_Click(object sender, EventArgs e) //pressed delete
        {
            // Start a local transaction
            MySqlTransaction myTrans = conn.BeginTransaction();

            MySqlCommand cmd = new MySqlCommand(); //start new command
            cmd.Connection = conn;
            cmd.Transaction = myTrans;
            cmd.CommandTimeout = 5;
            string n = this.Controls["delete_book_tags_res"].Controls["delete_book_tags_res_inp"].Text;
            string t = this.Controls["delete_book_tags_res"].Controls["delete_book_tags_res_inp2"].Text;

            try
            {
                cmd.CommandText = "select id_books from books where name = @name limit 1"; //get book id
                cmd.Parameters.AddWithValue("@name", n);
                cmd.Prepare();
                int book_id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "select id_tags from tags where name = @nam limit 1"; //get book id
                cmd.Parameters.AddWithValue("@nam", t);
                cmd.Prepare();
                int tag_id = Int32.Parse(cmd.ExecuteScalar().ToString());

                cmd.CommandText = "Delete from book_tags where fk_book_id = @b_id and fk_tag_id = @t_id"; //delete from book_tags
                cmd.Parameters.AddWithValue("@b_id", book_id);
                cmd.Parameters.AddWithValue("@t_id", tag_id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                myTrans.Commit();
                MessageBox.Show("Deleted tag of a book from book tags list successfully.");
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException exc)
                {
                    MessageBox.Show(exc.ToString());
                }

                MessageBox.Show("An exception of type " + ex.GetType() +
                " was encountered while inserting the data. \nEverything has been rolled back.");
                MessageBox.Show(ex.ToString());
            }
        }

        private void interaction5_Click(object sender, EventArgs e) //log in
        {
            HideAll_ShowOne("login_inp");
            this.Controls["login_inp"].Controls["login_name_l"].Text = "Account name:";
            this.Controls["login_inp"].Controls["login_name_inp"].Text = "";
            this.Controls["login_inp"].Controls["login_pass_l"].Text = "Password:";
            this.Controls["login_inp"].Controls["login_pass_inp"].Text = "";
        }

        private void login_inp0_Click(object sender, EventArgs e) //log in pressed
        {
            string name = this.Controls["login_inp"].Controls["login_name_inp"].Text;
            string pass = this.Controls["login_inp"].Controls["login_pass_inp"].Text;
            string myConnectionString = "server=127.0.0.1; port=3307; uid=" + name + "; pwd=" + pass + "; database=bookkeeper";
            conn = new MySqlConnection(myConnectionString);

            try
            {
                MessageBox.Show("Logged in successfully.");
                user = name;
                this.Controls["logged_as"].Controls["logged_as_l"].Text = "Logged in as " + name;
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void interaction6_Click(object sender, EventArgs e) //backup
        {
            if (user == "bookadmin" || user == "root")
            {
                HideAll_ShowOne("backup");
            }
            else
            {
                MessageBox.Show("Need to log in as admin to backup or rollback database.");
            }
        }

        private void backup0_Click(object sender, EventArgs e) //backup db
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C mysqldump --user=bookadmin --password=admin1234 --host=localhost --port=3307 bookkeeper > C:\\Users\\Rudy\\Documents\\bookkeeper.sql";
                process.StartInfo = startInfo;
                process.Start();                

                MessageBox.Show("Backup successfull.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void backup1_Click(object sender, EventArgs e) //rollback db
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "/C mysqldump --user=bookadmin --password=admin1234 --host=localhost --port=3307 bookkeeper < C:\\Users\\Rudy\\Documents\\bookkeeper.sql";
                process.StartInfo = startInfo;
                process.Start();

                MessageBox.Show("Rollback to backup successfull.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
