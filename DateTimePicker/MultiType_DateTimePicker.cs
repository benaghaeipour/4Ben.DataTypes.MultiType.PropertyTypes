using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using umbraco.uicontrols.DatePicker;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    [PropertyType("A8EBDBE4-8D31-49E3-B0D1-9B867CB210EA", "Date Time Picker")]
    public class MultiType_DateTimePicker : AbstractPropertyType
    {
        #region PrevalueEditorFields
        CheckBox chkPrevalueShowTime;
        bool boolShowTime;
        #endregion

        #region DataEditorFields
        umbraco.uicontrols.DatePicker.DateTimePicker dtpDataEditor;
        #endregion

        public override bool hasPrevalueControls()
        {
            return true;
        }

        public override Control PrevalueControls(int MultiTypeId, Dictionary<string, object> properties)
        {
            Panel pnlMultiType_textbox = new Panel();

            Label lblPrevalueEditorMandatory = new Label() { Text = "Show Time?" };

            chkPrevalueShowTime = new CheckBox();

            if (properties != null && properties.Count > 0)
            {
                if (properties.ContainsKey("ShowTime"))
                {
                    chkPrevalueShowTime.Checked = (bool)properties["ShowTime"];
                }
            }

            pnlMultiType_textbox.Controls.Add(lblPrevalueEditorMandatory);
            pnlMultiType_textbox.Controls.Add(chkPrevalueShowTime);

            return pnlMultiType_textbox;
        }

        public override Dictionary<string, object> SavePrevalueControl(int MultiTypeId)
        {
            var additionalProperties = new Dictionary<string, object>();

            additionalProperties.Add("ShowTime", chkPrevalueShowTime.Checked);

            return additionalProperties;
        }

        public override string PrevalueCSS()
        {
            return "";
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

            dtpDataEditor = new umbraco.uicontrols.DatePicker.DateTimePicker();

            if (properties.ContainsKey("ShowTime"))
            {
                boolShowTime = bool.Parse(properties["ShowTime"].ToString());
                dtpDataEditor.ShowTime = boolShowTime;
            }

            if (xml != null)
            {
                //Anything special about the xml? no - just do innertext
                dtpDataEditor.DateTime = Convert.ToDateTime(xml.InnerText);
            }
            
            pnlDataEditor.Controls.Add(lblDataEditor);
            pnlDataEditor.Controls.Add(litDescription);
            pnlDataEditor.Controls.Add(dtpDataEditor);

            if (xml != null)
            {
                //Anything special about the xml? no - just do innertext
                dtpDataEditor.DateTime = Convert.ToDateTime(xml.InnerText);
            }

            return pnlDataEditor;
        }

        public override string DataEditorCSS()
        {
            return "";
        }

        public override XmlNode SaveDataEditorControl(ref XmlDocument doc)
        {

            XmlCDataSection textNode;

            if (boolShowTime)
            {
                textNode = doc.CreateCDataSection(dtpDataEditor.DateTime.ToString());
            }
            else
            {
                textNode = doc.CreateCDataSection(dtpDataEditor.DateTime.ToShortDateString());
            }

            return textNode;
        }

        public override void LoadDataEditorControl(XmlNode item)
        {
            if (item != null)
            {
                dtpDataEditor.DateTime = Convert.ToDateTime(item.InnerText);
                dtpDataEditor.ShowTime = boolShowTime;
            }
        }

        public override void ResetDataEditorConrol()
        {
            dtpDataEditor.DateTime = DateTime.Now;
            dtpDataEditor.ShowTime = boolShowTime;
        }
    }
}