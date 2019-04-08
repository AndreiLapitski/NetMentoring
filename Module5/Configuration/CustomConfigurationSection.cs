using System.Configuration;
using System.Globalization;

namespace Task5.Configuration
{
    public class CustomConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("cultureName")]
        public CurrentCulture Culture => (CurrentCulture)this["cultureName"];

        [ConfigurationCollection(typeof(FolderElement),
            AddItemName = "folder")]
        [ConfigurationProperty("listeningFolders")]
        public FolderElementCollection ListeningFolders => (FolderElementCollection)this["listeningFolders"];

        [ConfigurationCollection(typeof(RuleElement),
            AddItemName = "rule")]
        [ConfigurationProperty("rules")]
        public RuleElementCollection Rules => (RuleElementCollection)this["rules"];
    }

    public class CurrentCulture : ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public CultureInfo Culture => (CultureInfo)this["name"];
    }

    public class FolderElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)element).FolderName;
        }
    }    

    public class RuleElementCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultFolder",
            IsRequired = true)]
        public string DefaultFolder => (string)this["defaultFolder"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).Id;
        }
    }

    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("name",
            IsKey = true,            
            IsRequired = true)]
        public string FolderName => (string)base["name"];

        [ConfigurationProperty("path")]
        public string DestinationPath => (string)base["path"];
    }

    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("id",
            IsKey = true,
            IsRequired = true)]
        public int Id => (int)base["id"];

        [ConfigurationProperty("pattern",
            IsRequired = true)]
        public string Pattern => (string)base["pattern"];

        [ConfigurationProperty("destinationFolder",
            IsRequired = true)]
        public string DestinationFolder => (string)base["destinationFolder"];

        [ConfigurationProperty("addEnumeration",
            IsRequired = true)]
        public bool AddEnumeration => (bool)base["addEnumeration"];

        [ConfigurationProperty("addTransferDate",
            IsRequired = true)]
        public bool AddTransferDate => (bool)base["addTransferDate"];
    }
}
