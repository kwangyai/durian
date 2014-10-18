using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SmartLib.Xml
{
    public class DmXmlNode
    {
        public XmlNode Node;

        public DmXmlNode(XmlNode InpNode)
        {
            Node = InpNode;
        }

        public int GetChildsCount()
        {
            if (Node != null)
                return Node.ChildNodes.Count;
            return 0;
        }

        public int GetChildsCount(string Name)
        {
            if (Node != null)
            {
                int CurIndex = 0;
                foreach (XmlNode CurNode in Node.ChildNodes)
                {
                    if (CurNode.Name == Name)
                        CurIndex++;
                }
                return CurIndex;
            }
            return 0;
        }

        public DmXmlNode GetChildNode(int Index)
        {
            if (Node != null)
            {
                if (Index < GetChildsCount())
                    return new DmXmlNode(Node.ChildNodes[Index]);
            }
            return null;
        }

        public DmXmlNode GetChildNode(string Name, int Index)
        {
            if (Node != null)
            {
                int CurIndex = 0;
                foreach (XmlNode CurNode in Node.ChildNodes)
                {
                    if (CurNode.Name == Name)
                    {
                        if (CurIndex == Index)
                            return new DmXmlNode(CurNode);
                        CurIndex++;
                    }
                }
            }
            return null;
        }

        public DmXmlNode[] GetChildNodes(string Name)
        {
            List<DmXmlNode> Nodes = new List<DmXmlNode>();
            if (Node != null)
            {
                int CurIndex = 0;
                foreach (XmlNode CurNode in Node.ChildNodes)
                {
                    if (CurNode.Name == Name)
                    {
                        Nodes.Add(new DmXmlNode(CurNode));
                    }
                }
            }
            return Nodes.ToArray();
        }

        public string Name
        {
            get { if (Node != null) return Node.Name; ; return null; }
        }

        public string Value
        {
            get { if (Node != null) return Node.InnerXml; return null; }
            set { if (Node != null) Node.InnerXml = value; }
        }

        public string Text
        {
            get { if (Node != null) return Node.InnerText; return null; }
            set { if (Node != null) Node.InnerText = value; }
        }

        public string GetAttribute(string Name)
        {
            return GetAttribute(Name, "");
        }

        public string GetAttribute(string Name, string Default)
        {
            if (Node != null)
            {
                try
                {
                    XmlAttribute CurAttrib = Node.Attributes[Name];
                    return CurAttrib.InnerText;
                }
                catch { }
            }
            return Default;
        }

        public bool SetAttribute(string Name, string Text)
        {
            if (Node != null)
                try
                {
                    XmlAttribute CurAttrib = Node.Attributes[Name];
                    if (CurAttrib != null)
                    {
                        CurAttrib.InnerText = Text;
                        return true;
                    }

                    CurAttrib = Node.OwnerDocument.CreateAttribute(Name);
                    CurAttrib.InnerText = Text;

                    Node.Attributes.Append(CurAttrib);
                    return true;
                }
                catch { }
            return false;
        }


    }

    public class DmXmlDocument : XmlDocument
    {
        string XmlFName;

        #region Contructor
        public DmXmlDocument() { }

        public DmXmlDocument(string FName, bool EnLoad)
        {
            XmlFName = FName;
            this.Load();
        }
        #endregion

        #region Load File
        public bool Load()
        {
            try
            {
                base.Load(XmlFName);
                return true;
            }
            catch (Exception Ex) { }
            return false;
        }

        public new bool Load(string FName)
        {
            XmlFName = FName;
            return this.Load();
        }
        #endregion

        #region Save File
        public bool Save()
        {
            try
            {
                base.Save(XmlFName);
                return true;
            }
            catch { }
            return false;
        }

        public new bool Save(string FName)
        {
            XmlFName = FName;
            return this.Save();
        }
        #endregion


        public DmXmlNode GetFirstChild()
        {
            return new DmXmlNode(base.FirstChild);
        }

        public DmXmlNode GetLastChild()
        {
            return new DmXmlNode(base.LastChild);
        }


        // Get Root Node (currently is Root)
        public DmXmlNode GetChildNode(string Name, int Index)
        {
            int CurIndex = 0;
            foreach (XmlNode CurNode in this.ChildNodes)
            {
                if (CurNode.Name == Name)
                {
                    if (CurIndex == Index)
                        return new DmXmlNode(CurNode);
                    CurIndex++;
                }
            }
            return null;
        }

        public new DmXmlNode SelectSingleNode(string XPath)
        {
            XmlNode Res = base.SelectSingleNode(XPath);
            if (Res != null)
            {
                return new DmXmlNode(Res);
            }
            return null;
        }





        public DmXmlNode CurrentNode, LastNode;

        public DmXmlNode SetCurrent(DmXmlNode NewCurrent)
        {
            LastNode = CurrentNode;
            return CurrentNode = NewCurrent;
        }

        public bool SelBackward()
        {
            if (LastNode == null)
                return false;
            CurrentNode = LastNode;
            LastNode = null;
            return true;
        }

        public bool SelRootNode(string Name, int Index)
        {
            DmXmlNode CurNode = this.GetChildNode(Name, Index);
            if (CurNode != null)
            {
                SetCurrent(CurNode);
                return true;
            }
            return false;
        }

        public bool SelChildNode(string Name, int Index)
        {
            if (CurrentNode != null)
            {
                DmXmlNode CurNode = CurrentNode.GetChildNode(Name, Index);
                if (CurNode != null)
                {
                    SetCurrent(CurNode);
                    return true;
                }
            }
            return false;
        }

        public string this[string Name]
        {
            get { return SelNode_GetChildText(Name, 0, ""); }
            set { SelNode_SetChildText(Name, 0, value); }
        }

        public string this[string Name, int Index]
        {
            get { return SelNode_GetChildText(Name, Index, ""); }
            set { SelNode_SetChildText(Name, Index, value); }
        }

        public string SelNode_GetChildText(string Name) { return SelNode_GetChildText(Name, 0, ""); }

        public string SelNode_GetChildText(string Name, int Index, string Def)
        {
            if (CurrentNode != null)
            {
                DmXmlNode CurNode = CurrentNode.GetChildNode(Name, Index);
                if (CurNode != null)
                    return CurNode.Text;
            }
            return Def;
        }


        public bool SelNode_SetChildText(string Name, int Index, string Value)
        {
            if (CurrentNode != null)
            {
                DmXmlNode CurNode = CurrentNode.GetChildNode(Name, Index);
                if (CurNode != null)
                {
                    CurNode.Text = Value;
                    return true;
                }
            }
            return false;
        }


        public string SelNode_GetChildAttribute(string Name, int Index, string AttribName, string Def)
        {
            if (CurrentNode != null)
            {
                DmXmlNode CurNode = CurrentNode.GetChildNode(Name, Index);
                if (CurNode != null)
                {
                    return CurNode.GetAttribute(AttribName);
                }
            }
            return Def;
        }
    }
}
