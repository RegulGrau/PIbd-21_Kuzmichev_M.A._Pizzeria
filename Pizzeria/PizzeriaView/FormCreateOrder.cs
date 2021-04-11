using PizzeriaBuisnessLogic.BindingModels;
using PizzeriaBuisnessLogic.BuisnessLogics;
using PizzeriaBuisnessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;


namespace PizzeriaView
{
    public partial class FormCreateOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly PizzaLogic _logicP;
        private readonly OrderLogic _logicO;
        public FormCreateOrder(PizzaLogic logicP, OrderLogic logicO)
        {
            InitializeComponent();
            _logicP = logicP;
            _logicO = logicO;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                int x = _logicP.Read(null).Count;
                string[] v = new string[x];
                for (int i = 0; i < x; i++)
                    v[i] = _logicP.Read(null)[i].PizzaName;
                comboBoxPizza.Items.AddRange(v);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxPizza.SelectedItem != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxPizza.SelectedIndex);
                    PizzaViewModel pizza = _logicP.Read(new PizzaBindingModel
                    {
                        Id
                    = id+1
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * pizza?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ComboBoxPizza_SelectedIndexChanged(object sender, EventArgs e)
        {

            CalcSum();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Fill in the Quantity field", "Error",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPizza.SelectedItem == null)
            {
                MessageBox.Show("Choose Pizza", "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                int x=0;
                for (int i = 0; i < _logicP.Read(null).Count; i++)
                    if (comboBoxPizza.SelectedItem.ToString() == _logicP.Read(null)[i].PizzaName)
                        x = _logicP.Read(null)[i].Id;
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    
                    PizzaId = x,
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                }) ;
                MessageBox.Show("Save was successful", "Message",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}