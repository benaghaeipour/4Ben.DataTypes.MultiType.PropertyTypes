using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.PropertyTypes.Textbox.css.PrevalueEditor.css", "text/css", PerformSubstitution = true)]
[assembly: System.Web.UI.WebResource("_4Ben.DataTypes.MultiType.PropertyTypes.Textbox.css.DataEditor.css", "text/css", PerformSubstitution = true)]
namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    [ValidationProperty("IsValid")]
    [PropertyType("5E126F7A-823B-4105-9582-8A5278CE13C7", "Textbox")]
    public class MultiType_Textbox : AbstractPropertyType
    {
        #region PrevalueEditorFields
            CheckBox chkPrevalueMandatory;
            TextBox txtPrevalueValidation;
        #endregion

        #region DataEditorFields
            TextBox txtDataEditor;
        #endregion
        
        public MultiType_Textbox()
        {
        }

        public override string PrevalueCSS()
        {
            return "_4Ben.DataTypes.MultiType.PropertyTypes.Textbox.css.PrevalueEditor.css";
        }

        public override string DataEditorCSS()
        {
            return "_4Ben.DataTypes.MultiType.PropertyTypes.Textbox.css.DataEditor.css";
        }

        public override bool hasPrevalueControls()
        {
            return true;
        }

        public override bool hasDataEditorControls()
        {
            return true;
        }

        public override Control PrevalueControls(int MultiTypeId, Dictionary<string, object> properties)
        {
            Panel pnlMultiType_textbox = new Panel();
            
            Label lblPrevalueEditorMandatory = new Label() { Text = "Mandatory" };
            Label lblPrevalueEditorValidation = new Label() { Text = "Validation" };

            chkPrevalueMandatory = new CheckBox();
            txtPrevalueValidation = new TextBox();

            if (properties != null && properties.Count > 0)
            {
                chkPrevalueMandatory.Checked = (bool)properties["Mandatory"];
                txtPrevalueValidation.Text = (string)properties["Validation"];
            }

            pnlMultiType_textbox.Controls.Add(lblPrevalueEditorMandatory);
            pnlMultiType_textbox.Controls.Add(chkPrevalueMandatory);
            pnlMultiType_textbox.Controls.Add(lblPrevalueEditorValidation);
            pnlMultiType_textbox.Controls.Add(txtPrevalueValidation);

            return pnlMultiType_textbox;
        }

        public override Dictionary<string, object> SavePrevalueControl(int MultiTypeId)
        {
            var additionalProperties = new Dictionary<string, object>();

            additionalProperties.Add("Mandatory", chkPrevalueMandatory.Checked);
            additionalProperties.Add("Validation", txtPrevalueValidation.Text);

            return additionalProperties;
        }

        public override Control DataEditorControls(XmlNode xml, Dictionary<string, object> properties)
        {
            //properties should be multiType properties ie. Name, Description, Mandatory, Validation
            Panel pnlDataEditor = new Panel();

            Label lblDataEditor = new Label() { Text = properties["Name"].ToString() };
            Literal litDescription = new Literal() { Text = properties["Description"].ToString() };

            txtDataEditor = new TextBox();
                               
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

        public override bool IsValid(Dictionary<string, object> properties = null)
        {
            return true;
        }
    }
}