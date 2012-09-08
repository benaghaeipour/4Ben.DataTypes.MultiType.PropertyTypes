using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using umbraco.uicontrols.TreePicker;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;

namespace _4Ben.DataTypes.MultiType.PropertyTypes
{
    [PropertyType("B9866C43-E554-4948-970B-83C7E40A6C50", "Media Picker")]
    public class MultiType_MediaPicker : AbstractPropertyType
    {
        #region DataEditorFields
        SimpleMediaPicker mpDataEditor;
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

            mpDataEditor = new SimpleMediaPicker();

            pnlDataEditor.Controls.Add(lblDataEditor);
            pnlDataEditor.Controls.Add(litDescription);
            pnlDataEditor.Controls.Add(mpDataEditor);

            if (xml != null)
            {
                //Anything special about the xml? no - just do innertext
                mpDataEditor.Value = xml.InnerText;
            }

            return pnlDataEditor;
        }

        public override string DataEditorCSS()
        {
            return "";
        }

        public override XmlNode SaveDataEditorControl(ref XmlDocument doc)
        {
            XmlCDataSection textNode = doc.CreateCDataSection(mpDataEditor.Value.ToString());

            return textNode;
        }

        public override void LoadDataEditorControl(XmlNode item)
        {
            if (item != null)
            {
                mpDataEditor.Value = item.InnerText;
            }
        }

        public override void ResetDataEditorConrol()
        {
            mpDataEditor.Value = string.Empty;
        }
    }
}