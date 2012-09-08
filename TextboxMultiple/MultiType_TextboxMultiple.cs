using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.PropertyTypes.TextboxMultiple.css.Developer.css", "text/css", PerformSubstitution = true)]
[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.PropertyTypes.TextboxMultiple.css.ContentEditor.css", "text/css", PerformSubstitution = true)]
namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    [PropertyType("9FEFBBBF-EAF3-4FD5-ACBD-6B70BC0293A0", "Textbox Multiple")]
    public class MultiType_TextboxMultiple : AbstractPropertyType
    {

        #region PrevalueEditorFields
        #endregion

        #region DataEditorFields
        TextBox txtDataEditor;
        #endregion


        public MultiType_TextboxMultiple()
        {
        }

        public override string PrevalueCSS()
        {
            return "_4Ben.DataTypes.MultiType.PropertyTypes.TextboxMultiple.css.PrevalueEditor.css";
        }        
        
        public override bool hasPrevalueControls()
        {
            return false;
        }

        public override Control PrevalueControls(int MultiTypeId, Dictionary<string, object> properties)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, object> SavePrevalueControl(int MultiTypeId)
        {
            throw new NotImplementedException();
        }

        public override string DataEditorCSS()
        {
            return "_4Ben.DataTypes.MultiType.PropertyTypes.TextboxMultiple.css.DataEditor.css";
        }

        public override bool hasDataEditorControls()
        {
            return true;
        }

        public override Control DataEditorControls(XmlNode xml, Dictionary<string, object> properties)
        {
            //properties should be multiType properties ie. Name, Description, Mandatory, Validation
            Panel pnlDataEditor = new Panel();

            Label lblDataEditor = new Label() { Text = properties["Name"].ToString() };
            Literal litDescription = new Literal() { Text = properties["Description"].ToString() };

            txtDataEditor = new TextBox() { TextMode = TextBoxMode.MultiLine };

            pnlDataEditor.Controls.Add(lblDataEditor);
            pnlDataEditor.Controls.Add(litDescription);
            pnlDataEditor.Controls.Add(txtDataEditor);

            if (xml != null)
            {
                //Anything special about the xml? no - just do innertext
                txtDataEditor.Text = xml.InnerText;
            }

            return pnlDataEditor;
        }

        public override XmlNode SaveDataEditorControl(ref XmlDocument doc)
        {
            XmlCDataSection textNode = doc.CreateCDataSection(txtDataEditor.Text);

            return textNode;
        }

        public override void LoadDataEditorControl(XmlNode item)
        {
            if (item != null)
            {
                txtDataEditor.Text = item.InnerText;
            }
        }

        public override void ResetDataEditorConrol()
        {
            txtDataEditor.Text = string.Empty;
        }
    }
}