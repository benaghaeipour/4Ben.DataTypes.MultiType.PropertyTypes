using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;

using umbraco.uicontrols.TreePicker;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    [PropertyType("327F6C28-157D-4C5F-9C08-8985776BD329", "Content Picker")]
    public class MultiType_ContentPicker : AbstractPropertyType
    {

        #region DataEditorFields
        SimpleContentPicker cpDataEditor;
        #endregion

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

        public override string PrevalueCSS()
        {
            throw new NotImplementedException();
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

            cpDataEditor = new SimpleContentPicker();

            pnlDataEditor.Controls.Add(lblDataEditor);
            pnlDataEditor.Controls.Add(litDescription);
            pnlDataEditor.Controls.Add(cpDataEditor);

            if (xml != null)
            {
                //Anything special about the xml? no - just do innertext
                cpDataEditor.Value = xml.InnerText;
            }

            return pnlDataEditor;
        }

        public override string DataEditorCSS()
        {
            return "";
        }

        public override XmlNode SaveDataEditorControl(ref XmlDocument doc)
        {
            XmlCDataSection textNode = doc.CreateCDataSection(cpDataEditor.Value);

            return textNode;
        }

        public override void LoadDataEditorControl(XmlNode item)
        {
            if (item != null)
            {
                cpDataEditor.Value = item.InnerText;
            }
        }

        public override void ResetDataEditorConrol()
        {
            cpDataEditor.Value = string.Empty;
        }
    }
}