using System;
using System.Reflection;
using System.Web.UI.WebControls;

namespace StonePayments.Util.ViewWeb
{
    public class GridViewEntityModel : GridView
    {
        public Type TypeEntityModel { get; set; }

        public override object DataSource
        {
            get => base.DataSource;
            set
            {
                DataKeyNames = new string[] { "Id" };

                AutoGenerateColumns = false;

                Columns.Clear();

                AddColCommandButtons();

                AddFieldColumn();

                base.DataSource = value;

                DataBind();
            }
        }

        private void AddFieldColumn()
        {
            foreach(var property in TypeEntityModel.GetProperties())
            {
                if(property.Name.ToLower() != "id")
                {
                    Columns.Add(new BoundField
                    {   DataField = property.Name,
                        HeaderText = property.Name
                    });
                }
            }
        }

        private void AddColCommandButtons()
        {
            CommandField btnExcluir = new CommandField();
            btnExcluir.ButtonType = ButtonType.Button;
            btnExcluir.HeaderText = "Delete";
            btnExcluir.DeleteText = "Delete";
            btnExcluir.ShowDeleteButton = true;

            Columns.Add(btnExcluir);

            CommandField btnEditar = new CommandField();
            btnEditar.ButtonType = ButtonType.Button;
            btnEditar.HeaderText = "Edit";
            btnEditar.EditText = "Edit";
            btnEditar.ShowEditButton = true;

            Columns.Add(btnEditar);
        }
    }
}
