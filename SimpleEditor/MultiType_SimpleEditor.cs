using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.editorControls.simpleEditor;
using System.Xml;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    [PropertyType("6C412EED-452D-474E-AE76-9921BB078A95", "Simple Editor")]
    public class MultiType_SimpleEditor : AbstractPropertyType
    {
        #region PrevalueEditorFields
            CheckBox chkPrevalueMandatory;
            TextBox txtPrevalueValidation;
        #endregion

        #region DataEditorFields
            SimpleEditor seDataEditor;
        #endregion

        public MultiType_SimpleEditor()
        {
        }

        public override string PrevalueCSS()
        {
            return "";
        }

        public override string DataEditorCSS()
        {
            return "";
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

            seDataEditor = new SimpleEditor(null);
                        
            pnlDataEditor.Controls.Add(lblDataEditor);
            pnlDataEditor.Controls.Add(litDescription);
            pnlDataEditor.Controls.Add(seDataEditor);
            
            if (xml != null)
            {
                //Anything special about the xml? no - just do innertext
                seDataEditor.Text = xml.InnerText;
            }

            return pnlDataEditor;
        }

        public override XmlNode SaveDataEditorControl(ref XmlDocument doc)
        {            
            XmlCDataSection textNode = doc.CreateCDataSection(seDataEditor.Text);

            return textNode;
        }

        public override void LoadDataEditorControl(XmlNode item)
        {
            if (item != null)
            {
                seDataEditor.Text = item.InnerText;
            }
        }

        public override void ResetDataEditorConrol()
        {
            seDataEditor.Text = string.Empty;
        }

        public override bool IsValid(Dictionary<string, object> properties = null)
        {
            return true;
        }
    }
}