using RDB.Data.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDB.UI
{
    public partial class Main : Form
    {
        #region Fields

        private readonly DefaultContext defaultContext;

        #endregion

        #region Constructors

        public Main()
        {
            defaultContext = new DefaultContext();

            InitializeComponent();
        }

        #endregion
    }
}
