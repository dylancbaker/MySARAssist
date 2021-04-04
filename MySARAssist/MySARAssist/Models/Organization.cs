using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.Models
{
    public class Organization
    {
        public Organization() { }
        public Organization(Guid id, string name) { OrganizationID = id; OrganizationName = name; }

        private Guid _organizationID;
        public Guid OrganizationID { get => _organizationID; set => _organizationID = value; }
        private string _organizationName;
        public string OrganizationName { get => _organizationName; set => _organizationName = value; }
        private string _primaryEmail;
        public string PrimaryEmail { get => _primaryEmail; set => _primaryEmail = value; }
        private string _primaryPassword;
        public string PrimaryPassword { get => _primaryPassword; set => _primaryPassword = value; }
        private int _userCount = 0;
        public int UserCount { get => _userCount; set => _userCount = value; }


        public List<Organization> getStaticOrganizationList()
        {
            //to be used when the org list is needed and internet is not avilable;
            List<Organization> organizations = new List<Organization>();

            organizations.Add(new Organization(new Guid("46698CE0-B146-40E2-B834-0089DECE3896"), "Mackenzie SAR"));
            organizations.Add(new Organization(new Guid("9202836C-DC5C-4C62-8190-022A03769098"), "Revelstoke SAR"));
            organizations.Add(new Organization(new Guid("BE921E46-2521-4B8C-9438-0645CCB19DE8"), "Arrowsmith SAR"));
            organizations.Add(new Organization(new Guid("2F07BE02-EC9F-435D-B8E2-0B31799879BC"), "Quesnel SAR"));
            organizations.Add(new Organization(new Guid("C111498E-74F0-46B3-8442-0B38D435C854"), "Juan De Fuca SAR"));
            organizations.Add(new Organization(new Guid("16FAD27A-76E6-471E-A3FA-146CCB740DBC"), "Kent Harrison SAR"));
            organizations.Add(new Organization(new Guid("4A2BA222-3766-4F38-86E4-1724FDE94AF3"), "Logan Lake SAR"));
            organizations.Add(new Organization(new Guid("5C8A48A2-62F6-44E8-93CC-18ECAAE7E2ED"), "Arrow Lakes SAR"));
            organizations.Add(new Organization(new Guid("DBCE7252-92D1-481F-ADD0-1923F1F7FD39"), "Castlegar SAR"));
            organizations.Add(new Organization(new Guid("F2874DE5-0066-45A5-A3F8-1A38A64D67EC"), "Cowichan SAR"));
            organizations.Add(new Organization(new Guid("499A9B5C-E712-4968-8B9F-1DEB9D402915"), "Campbell River SAR"));
            organizations.Add(new Organization(new Guid("0EBB892E-F241-466A-B1E1-1E05F01EF0C7"), "North Shore SAR"));
            organizations.Add(new Organization(new Guid("52078C4B-4A25-4103-A7EA-224B68AB5C55"), "West Chilcotin SAR"));
            organizations.Add(new Organization(new Guid("BFDAB6B8-BEE2-4157-A144-23965C166BD8"), "Stewart SAR"));
            organizations.Add(new Organization(new Guid("E05BB4DE-8786-4289-B39B-2574C41E2777"), "Houston SAR"));
            organizations.Add(new Organization(new Guid("76A1101B-464D-4D12-ABBC-266599F2F5DD"), "Robson Valley SAR"));
            organizations.Add(new Organization(new Guid("D44C628D-E9AD-43CE-9671-2965C64FF0E5"), "Fernie SAR"));
            organizations.Add(new Organization(new Guid("F964C5E1-2337-4761-AB36-2965F57E7DC4"), "Atlin SAR"));
            organizations.Add(new Organization(new Guid("62231DA2-DCB8-413E-8C60-2A0B2E8DC214"), "Parkland SAR"));
            organizations.Add(new Organization(new Guid("37F43ECF-9CD5-4EB5-9699-2C0DBB7974F3"), "Ladysmith SAR"));
            organizations.Add(new Organization(new Guid("5B2C5A11-07DC-4104-99A5-364C204D4B32"), "Nicola Valley SAR"));
            organizations.Add(new Organization(new Guid("937148E6-72FD-456B-9DD9-38E975492D5F"), "Nanaimo SAR"));
            organizations.Add(new Organization(new Guid("124CA510-0BD2-4F53-82BE-3A5F4960BFCC"), "Penticton SAR"));
            organizations.Add(new Organization(new Guid("0B0DC8D4-9A62-4987-8EFA-43FC2A31D598"), "Whistler SAR"));
            organizations.Add(new Organization(new Guid("87756CEF-01AC-493A-89BE-464CADAE7EE1"), "Kimberley SAR"));
            organizations.Add(new Organization(new Guid("74E5E639-A831-427F-8494-4DCD0A55BF37"), "Creston Valley SAR"));
            organizations.Add(new Organization(new Guid("6A39C786-4C74-4B1E-91A6-4E1FCFE7584D"), "Kitimat SAR"));
            organizations.Add(new Organization(new Guid("7AF5AD55-2655-4218-B693-56F48353E190"), "South Columbia SAR"));
            organizations.Add(new Organization(new Guid("11A311E6-6768-46AA-9F3C-571AA95E3726"), "Nakusp SAR"));
            organizations.Add(new Organization(new Guid("65D6C93C-63F9-476F-8A63-5C8A968F4E11"), "Peninsula Emergency Measures Organization"));
            organizations.Add(new Organization(new Guid("D83B66D5-C697-448F-9E4F-5DA7828A66A5"), "Metchosin SAR"));
            organizations.Add(new Organization(new Guid("9893E045-D590-44E4-8AF4-5DBB1F1661A4"), "Prince Rupert SAR"));
            organizations.Add(new Organization(new Guid("B3AE6204-9E95-452A-9C10-5EB4178E0ABE"), "Pemberton SAR"));
            organizations.Add(new Organization(new Guid("2207EB6C-E292-47E8-AE3D-68855B846109"), "Powell River SAR"));
            organizations.Add(new Organization(new Guid("A1C1A217-4AE3-462C-A7D4-68FFAA8C0210"), "Rossland SAR"));
            organizations.Add(new Organization(new Guid("98851139-E736-4974-A672-6972F3A8965F"), "South Peace SAR"));
            organizations.Add(new Organization(new Guid("AB67B6D4-F795-43AD-9506-69880F7BEAFD"), "Alberni Valley Rescue Squad"));
            organizations.Add(new Organization(new Guid("8940A059-6A5A-4004-9D1E-758C4D576C2E"), "Bulkley Valley SAR"));
            organizations.Add(new Organization(new Guid("169C529D-792A-4615-ABE9-78203A6A70A4"), "Grand Forks SAR"));
            organizations.Add(new Organization(new Guid("88B20B0B-04BC-4596-A2F5-7CCCB317A4A0"), "Ridge Meadows SAR"));
            organizations.Add(new Organization(new Guid("FEC27732-35F1-43D6-9597-7E0D2FEEDD65"), "Mission SAR"));
            organizations.Add(new Organization(new Guid("CDA9B3A3-FC65-4803-8685-813BCE298DEF"), "Bella Coola Valley SAR"));
            organizations.Add(new Organization(new Guid("2EFA81F4-3E9B-4E45-BE94-8166AAA9298C"), "Keremeos SAR"));
            organizations.Add(new Organization(new Guid("FF7F03D5-E414-443F-8315-82BB378ABBD8"), "Comox Valley Search and Rescue"));
            organizations.Add(new Organization(new Guid("4643747E-FB2A-4230-B4B8-8506F9951DFF"), "Chetwynd SAR"));
            organizations.Add(new Organization(new Guid("B865EDF6-F0A7-4241-8CD4-8A5942C7B18A"), "Tumbler Ridge SAR"));
            organizations.Add(new Organization(new Guid("589F720D-A304-49CF-B40C-8E944D7C9D2F"), "Fort St. James SAR"));
            organizations.Add(new Organization(new Guid("96BA69A4-436C-4DA1-85B1-992E84C36019"), "Unassigned"));
            organizations.Add(new Organization(new Guid("893B590C-A159-4A89-9CA4-A3DF24D6ABFE"), "Sunshine Coast SAR"));
            organizations.Add(new Organization(new Guid("B04FE1E7-979F-40CC-892C-A74EEA295ECB"), "Kaslo SAR"));
            organizations.Add(new Organization(new Guid("8B8D2A86-91D6-4606-B2D7-A9EDF3C8EE35"), "Burns Lake SAR"));
            organizations.Add(new Organization(new Guid("8FB71ABC-4C5C-43A8-8E68-AA75DD2262EB"), "Vernon SAR"));
            organizations.Add(new Organization(new Guid("CE558295-B9E2-41A3-88F1-AE029EC1AE0D"), "Kamloops SAR"));
            organizations.Add(new Organization(new Guid("DF7FBC23-17F2-41AB-8DEB-AF00D27C5B7F"), "Oliver Osoyoos SAR"));
            organizations.Add(new Organization(new Guid("64B9D581-05FD-4D9B-ABFF-B24BFAFCEA2C"), "Lions Bay SAR"));
            organizations.Add(new Organization(new Guid("3CD654FA-8B48-4C75-BB75-B7834639990C"), "South Cariboo SAR"));
            organizations.Add(new Organization(new Guid("F7CD798B-52AD-423E-B66F-BD01D3B70D5D"), "Squamish SAR"));
            organizations.Add(new Organization(new Guid("D0AEC6BB-EE25-437B-8A8C-BEAA284FF685"), "Fort Nelson SAR"));
            organizations.Add(new Organization(new Guid("890789F2-9DB1-4785-BF00-BF051676B11E"), "Shuswap SAR"));
            organizations.Add(new Organization(new Guid("2679596A-3D96-4E74-913E-BF273235FC6F"), "Nechako Valley SAR"));
            organizations.Add(new Organization(new Guid("EA48BC49-A566-4248-A4B5-C1A6DBBEF00B"), "Central Fraser Valley SAR"));
            organizations.Add(new Organization(new Guid("12FDF8F5-4127-456E-9F6D-C6912161BE6F"), "Columbia Valley SAR"));
            organizations.Add(new Organization(new Guid("20A1A9F3-CB11-43F8-BEFE-C8933A566764"), "Prince George SAR"));
            organizations.Add(new Organization(new Guid("C704D0F8-AF89-4143-BD3E-CB4EAC7A7AA7"), "Central Okanagan SAR"));
            organizations.Add(new Organization(new Guid("EF91565B-77F8-428B-9D15-D0EAA4043A0E"), "Hope SAR"));
            organizations.Add(new Organization(new Guid("97717FBA-977C-49CD-B105-D55FA705AA14"), "North Peace SAR"));
            organizations.Add(new Organization(new Guid("83081A8A-6C30-45A8-B9B1-D625090148D6"), "Elkford SAR"));
            organizations.Add(new Organization(new Guid("6C4ED3C8-18D1-4F25-BEB1-D8C3B5272267"), "Coquitlam SAR"));
            organizations.Add(new Organization(new Guid("6FD79D61-ED33-46E1-A1AA-E3BC1F24A143"), "Chilliwack SAR"));
            organizations.Add(new Organization(new Guid("B8E1D50D-C5A4-4ECD-9B5A-E4DB0AEFA57C"), "Saanich SAR"));
            organizations.Add(new Organization(new Guid("C0A7C62C-8C76-43F5-9357-E65394CCA2CB"), "South Fraser SAR"));
            organizations.Add(new Organization(new Guid("452E4432-F01C-4FDB-8DB4-E7399FC09A97"), "Sparwood SAR"));
            organizations.Add(new Organization(new Guid("0E63E227-27A8-4B11-8043-EC2C589A4CBA"), "Golden SAR"));
            organizations.Add(new Organization(new Guid("3642094A-274C-44DA-B379-ED42E9265FF8"), "Central Cariboo SAR"));
            organizations.Add(new Organization(new Guid("D5A57651-6C40-4A8E-A442-F0D7294FE0ED"), "Terrace SAR"));
            organizations.Add(new Organization(new Guid("8CA3E11B-5A87-4C72-A11C-F14225AC7AAF"), "Princeton SAR"));
            organizations.Add(new Organization(new Guid("77F87A03-46E8-4C70-A05B-F32DAE58276B"), "Westcoast Inland SAR"));
            organizations.Add(new Organization(new Guid("8E824695-1EDD-49FE-BADF-F42F8A34A95F"), "Salt Spring Island SAR"));
            organizations.Add(new Organization(new Guid("5153C373-4B73-45F6-99B6-F4DA00D28B92"), "Wells Gray SAR"));
            organizations.Add(new Organization(new Guid("B2CD40D5-6ABF-4FF8-A89E-F5FE6B995C89"), "Nelson SAR"));
            organizations.Add(new Organization(new Guid("71FFF997-108B-4DDF-914E-F81069F8EA26"), "Barriere SAR"));
            organizations.Add(new Organization(new Guid("A3190007-E0EA-49F8-95F8-F8FF8396A38B"), "Cranbrook SAR"));
            organizations.Add(new Organization(new Guid("F1B9CA16-CB19-4DD2-961F-FE3EB6CC6477"), "Archipelago SAR"));
            organizations.Add(new Organization(new Guid("02035C34-CD9C-4B3D-9C22-5AF29068A0D9"), "Non-SAR"));
            organizations.Add(new Organization(new Guid("8CBE0C6D-78B1-4600-96C0-21E3C16A444D"), "Great Hat Web Design"));
            //02035C34-CD9C-4B3D-9C22-5AF29068A0D9

            return organizations;
        }
    }
}
