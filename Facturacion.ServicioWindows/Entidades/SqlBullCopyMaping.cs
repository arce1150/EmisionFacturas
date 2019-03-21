using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;

namespace Facturacion.ServicioWindows.Entidades
{
    public static class SqlBullCopyMaping
    {
        public static DataTable MapModel<T>(IEnumerable<T> models, String tableName) where T : class, new()
        {
            if (models == null || !models.Any())
            {
                return null;
            }
            else
            {
                DataTable dt = new DataTable(tableName);
                IEnumerable<PropertyInfo> propertyInfos = typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(ModelMapAttribute), true).Any());

                //crea columnas
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    ModelMapAttribute attribute = propertyInfo.GetCustomAttributes(typeof(ModelMapAttribute), true).FirstOrDefault() as ModelMapAttribute;
                    if (!String.IsNullOrWhiteSpace(attribute.ColumnName))
                    {
                        dt.Columns.Add(attribute.ColumnName);
                    }
                    else
                    {
                        dt.Columns.Add(propertyInfo.Name);
                    }
                }

                //inserta datos   
                foreach (var item in models)
                {
                    int matchCount = 0;
                    DataRow row = dt.NewRow();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        ModelMapAttribute attribute = propertyInfo.GetCustomAttributes(typeof(ModelMapAttribute), true).First() as ModelMapAttribute;
                        Object value = propertyInfo.GetValue(item, null);
                        row[!String.IsNullOrWhiteSpace(attribute.ColumnName) ? attribute.ColumnName : propertyInfo.Name] = value;

                        if (value != null)
                        {
                            matchCount++;
                        }
                    }

                    //Skip empty models  
                    if (matchCount > 0)
                    {
                        dt.Rows.Add(row);
                    }
                }
                dt.AcceptChanges();
                return dt;
            }
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ModelMapAttribute : Attribute
    {
        private string _columnName;
        public string ColumnName
        {
            get
            {
                return this._columnName;
            }
            set
            {
                this._columnName = value;
            }
        }
        public ModelMapAttribute(string columnName = null)
        {
            this._columnName = String.IsNullOrWhiteSpace(columnName) ? null : columnName;
        }
    }
}
