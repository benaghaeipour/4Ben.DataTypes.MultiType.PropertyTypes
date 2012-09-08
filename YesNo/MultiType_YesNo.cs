using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    [PropertyType("2B26BF3F-1070-4AFE-B382-F7B018974F63", "Yes No")]
    public class MultiType_YesNo : AbstractPropertyType
    {
        #region DataEditorFields
        CheckBox cbDataEditor;
        #endregion

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

            cbDataEditor = new CheckBox();

            pnlDataEditor.Controls.Add(lblDataEditor);
            pnlDataEditor.Controls.Add(litDescription);
            pnlDataEditor.Controls.Add(cbDataEditor);

            if (xml != null)
            {
                //Anything special about the xml? no - just do innertext
                cbDataEditor.Checked = bool.Parse(xml.InnerText);
            }

            return pnlDataEditor;
        }

        public override string DataEditorCSS()
        {
            return "";
        }

        public override XmlNode SaveDataEditorControl(ref XmlDocument doc)
        {
            XmlCDataSection textNode = doc.CreateCDataSection(cbDataEditor.Checked.ToString());

            return textNode;
        }

        public override void LoadDataEditorControl(XmlNode item)
        {
            if (item != null)
            {
                cbDataEditor.Checked = bool.Parse(item.InnerText);
            }
        }

        public override void ResetDataEditorConrol()
        {
            cbDataEditor.Checked = false;
        }
    }
}