using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;


namespace HKSupply.Helpers
{
    /// <summary>
    /// Extensiones personalizadas
    /// </summary>
    public static class CustomExtensions
    {
        /// <summary>
        /// Extensión para clonar objetos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(this T source)
        {
            var dcs = new DataContractSerializer(typeof(T));
            using (var ms = new System.IO.MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)dcs.ReadObject(ms);
            }
        }

        //----------------------------------------------------------------------------------

        /// <summary>
        /// Clonar un objecto usando reflection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objSource"></param>
        /// <returns></returns>
        /// <remarks>En pruebas</remarks>
        public static T CloneObject<T>(this T objSource)
        {
            //Get the type of source object and create a new instance of that type
            Type typeSource = objSource.GetType();
            object objTarget = Activator.CreateInstance(typeSource);

            //Get all the properties of source object type
            System.Reflection.PropertyInfo[] propertyInfo = typeSource.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //Assign all source property to taget object 's properties
            foreach (System.Reflection.PropertyInfo property in propertyInfo)
            {
                //Check whether property can be written to
                if (property.CanWrite)
                {
                    //check whether property type is value type, enum or string type
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType.Equals(typeof(System.String)))
                    {
                        property.SetValue(objTarget, property.GetValue(objSource, null), null);
                    }
                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                    else
                    {
                        object objPropertyValue = property.GetValue(objSource, null);
                        if (objPropertyValue == null)
                        {
                            property.SetValue(objTarget, null, null);
                        }
                        else
                        {
                            property.SetValue(objTarget, objPropertyValue.CloneObject(), null);
                        }
                    }
                }
            }
            return (T)objTarget;
        }
        //-----------------------------------------------------------------------------------

        /// <summary>
        /// Extensión para clonar listas de objetos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// Eliminar una fila de un TableLayoutPanel y todos los controles que contenga
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="rowIndex"></param>
        public static void RemoveRow(this TableLayoutPanel panel, int rowIndex)
        {
            panel.RowStyles.RemoveAt(rowIndex);

            for (int columnIndex = 0; columnIndex < panel.ColumnCount; columnIndex++)
            {
                var control = panel.GetControlFromPosition(columnIndex, rowIndex);
                panel.Controls.Remove(control);
            }

            for (int i = rowIndex + 1; i < panel.RowCount; i++)
            {
                for (int columnIndex = 0; columnIndex < panel.ColumnCount; columnIndex++)
                {
                    var control = panel.GetControlFromPosition(columnIndex, i);
                    panel.SetRow(control, i - 1);
                }
            }

            panel.RowCount--;
        }

        
        /// <summary>
        /// Extensión para hacer un binding tipado y evitar posibles errores al no depender del string para
        /// las propiedades bindeadas entre el objeto y el source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBindings"></param>
        /// <param name="dataSource"></param>
        /// <param name="controlLambda"></param>
        /// <param name="objectLambda"></param>
        /// <returns></returns>
        /// <remarks>Objetos sencillos que queramos bindear la propiedad Text</remarks>
        public static Binding Add<T>(this ControlBindingsCollection dataBindings, 
            object dataSource,
            Expression<Func<System.Windows.Forms.Control, object>> controlLambda,
            Expression<Func<T, object>> objectLambda)
        {
            string controlPropertyName;
            string bindingTargetName;

            if (controlLambda.Body is MemberExpression)
            {
                controlPropertyName = ((MemberExpression)(controlLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)controlLambda.Body).Operand;
                controlPropertyName = ((MemberExpression)op).Member.Name;
            }

            if (objectLambda.Body is MemberExpression)
            {
                bindingTargetName = ((MemberExpression)(objectLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)objectLambda.Body).Operand;
                bindingTargetName = ((MemberExpression)op).Member.Name;
            }

            return dataBindings.Add
                 (controlPropertyName, dataSource, bindingTargetName);
        }

        /// <summary>
        /// Extensión para hacer un binding tipado y evitar posibles errores al no depender del string para
        /// las propiedades bindeadas entre el objeto y el source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBindings"></param>
        /// <param name="dataSource"></param>
        /// <param name="controlLambda"></param>
        /// <param name="objectLambda"></param>
        /// <returns></returns>
        /// <remarks>Para CheckEdit de DevExpress</remarks>
        public static Binding Add<T>(this ControlBindingsCollection dataBindings,
            object dataSource,
            Expression<Func<CheckEdit, object>> controlLambda,
            Expression<Func<T, object>> objectLambda)
        {

            string controlPropertyName;
            string bindingTargetName;

            if (controlLambda.Body is MemberExpression)
            {
                controlPropertyName = ((MemberExpression)(controlLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)controlLambda.Body).Operand;
                controlPropertyName = ((MemberExpression)op).Member.Name;
            }


            if (objectLambda.Body is MemberExpression)
            {
                bindingTargetName = ((MemberExpression)(objectLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)objectLambda.Body).Operand;
                bindingTargetName = ((MemberExpression)op).Member.Name;
            }

            return dataBindings.Add
                 (controlPropertyName, dataSource, bindingTargetName);
        }

        /// <summary>
        /// Extensión para hacer un binding tipado y evitar posibles errores al no depender del string para
        /// las propiedades bindeadas entre el objeto y el source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBindings"></param>
        /// <param name="dataSource"></param>
        /// <param name="controlLambda"></param>
        /// <param name="objectLambda"></param>
        /// <returns></returns>
        /// <remarks>Para DateEdit de DevExpress</remarks>
        public static Binding Add<T>(this ControlBindingsCollection dataBindings,
            object dataSource,
            Expression<Func<DateEdit, object>> controlLambda,
            Expression<Func<T, object>> objectLambda)
        {

            string controlPropertyName;
            string bindingTargetName;

            if (controlLambda.Body is MemberExpression)
            {
                controlPropertyName = ((MemberExpression)(controlLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)controlLambda.Body).Operand;
                controlPropertyName = ((MemberExpression)op).Member.Name;
            }


            if (objectLambda.Body is MemberExpression)
            {
                bindingTargetName = ((MemberExpression)(objectLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)objectLambda.Body).Operand;
                bindingTargetName = ((MemberExpression)op).Member.Name;
            }

            return dataBindings.Add
                 (controlPropertyName, dataSource, bindingTargetName, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Extensión para hacer un binding tipado y evitar posibles errores al no depender del string para
        /// las propiedades bindeadas entre el objeto y el source
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataBindings"></param>
        /// <param name="dataSource"></param>
        /// <param name="controlLambda"></param>
        /// <param name="objectLambda"></param>
        /// <returns></returns>
        /// <remarks>Para LookUpEdit de DevExpress</remarks>
        public static Binding Add<T>(this ControlBindingsCollection dataBindings,
            object dataSource,
            Expression<Func<LookUpEdit, object>> controlLambda,
            Expression<Func<T, object>> objectLambda)
        {

            string controlPropertyName;
            string bindingTargetName;

            if (controlLambda.Body is MemberExpression)
            {
                controlPropertyName = ((MemberExpression)(controlLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)controlLambda.Body).Operand;
                controlPropertyName = ((MemberExpression)op).Member.Name;
            }


            if (objectLambda.Body is MemberExpression)
            {
                bindingTargetName = ((MemberExpression)(objectLambda.Body)).Member.Name;
            }
            else
            {
                var op = ((UnaryExpression)objectLambda.Body).Operand;
                bindingTargetName = ((MemberExpression)op).Member.Name;
            }

            return dataBindings.Add
                 (controlPropertyName, dataSource, bindingTargetName, true, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
