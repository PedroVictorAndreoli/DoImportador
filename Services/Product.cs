using DoImportador.Connection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoImportador.Services
{
    public class Product
    {

        private Form1 _form;

        public Product(Form1 form)
        {
            _form = form;
        }

        public void ImportData(List<IDictionary> data)
        {
            var iConn = new DOConn();

            try
            {
                iConn.ConnectionOpen("", Enum.EnumDataLake.DESTINATION);

                data.ForEach(dt => {

                    _form.OnSetLog($"Importou: {dt["Nome"]}");
                });

                MessageBox.Show("Dados importados com sucesso!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                iConn.ConnectionClose(iConn.DoConnection);
                iConn.Dispose();
            }
        }
    }
}
