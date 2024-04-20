﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MySARAssist.Models
{
    public class RADeMSCategory
    {
        private int _ID;
        private string _Name;
        private List<RADeMSQuestion> _Questions = new List<RADeMSQuestion>();
        public int ID { get { return _ID; } }
        public string Name { get { return _Name; } }
        public List<RADeMSQuestion> Questions { get => _Questions; private set => _Questions = value; }
        public RADeMSCategory() { }
        public RADeMSCategory(int id, string name, List<RADeMSQuestion> questions) { _ID = id; _Name = name; Questions = questions; }
    }


    public class RADeMSScore : SyncableItem, ICloneable
    {
         private Guid _SetByPositionID;
         private string _SetByName;
         private int[] _Scores = new int[10];
         private int _CategoryID;
       private string _Comment;
      private int _ManualOpRisk = -1;
       private int _ManualRespCap = -1;

        public DateTime LastUpdatedLocal { get => LastUpdatedUTC.ToLocalTime(); }
        public Guid SetByPositionID { get => _SetByPositionID; set => _SetByPositionID = value; }
        public string SetByName { get => _SetByName; set => _SetByName = value; }
        public int[] Scores { get => _Scores; set => _Scores = value; }
        public int CategoryID { get => _CategoryID; set => _CategoryID = value; }
        public string Comment { get => _Comment; set => _Comment = value; }
        public int ManualOpRisk { get => _ManualOpRisk; set => _ManualOpRisk = value; }
        public int ManualRespCap { get => _ManualRespCap; set => _ManualRespCap = value; }

        public RADeMSScore()
        {
            ID = Guid.NewGuid();
            LastUpdatedUTC = DateTime.UtcNow;
            Active = true;
        }

        public int OperationalRisk
        {
            get
            {
                if (ManualOpRisk >= 0) { return ManualOpRisk; }
                return CalculatedOperationalRisk;
            }
        }
        public int CalculatedOperationalRisk
        {
            get
            {
                return Scores[0] + Scores[1] + Scores[2] + Scores[3] + Scores[4];

            }
        }
        public int ResponseCapacity
        {
            get
            {
                if (ManualRespCap >= 0) { return ManualRespCap; }
                return CalculatedResponseCapacity;

            }
        }
        public int CalculatedResponseCapacity
        {
            get
            {
                return Scores[5] + Scores[6] + Scores[7] + Scores[8] + Scores[9];
            }
        }

        public string FullTextForLog
        {
            get
            {
                StringBuilder fullText = new StringBuilder();
                fullText.Append("RADeMS ");
                fullText.Append(" Op. Risk: "); fullText.Append(OperationalRisk);
                fullText.Append(", Response Cap.: "); fullText.Append(ResponseCapacity);
                fullText.Append(" | Conducted ");
                fullText.Append(LastUpdatedLocal.ToString("yyyy-MMM-dd HH:mm"));
                if (!string.IsNullOrEmpty(SetByName)) { fullText.Append(" by "); fullText.Append(SetByName); }
                if (!string.IsNullOrEmpty(Comment)) { fullText.Append(" "); fullText.Append(Comment); }
                return fullText.ToString();
            }
        }



        public string ShortText
        {
            get
            {
                StringBuilder fullText = new StringBuilder();
                fullText.Append(" Op. Risk: "); fullText.Append(OperationalRisk);
                fullText.Append(", Response Cap.: "); fullText.Append(ResponseCapacity);
                return fullText.ToString();
            }
        }

        public string VeryShortText
        {
            get
            {
                StringBuilder fullText = new StringBuilder();
                fullText.Append(OperationalRisk);
                fullText.Append(" x "); fullText.Append(ResponseCapacity);
                return fullText.ToString();

            }
        }
       

        public RADeMSScore Clone()
        {
            return this.MemberwiseClone() as RADeMSScore;
        }
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }



    public class RADeMSQuestion
    {
       private int _ID;
      private string _QuestionText;
       private bool _IsOperationalRisk;
     private string _EgOption1;
    private string _EgOption2;
     private string _EgOption3;
  private string _Option1Label;
    private string _Option2Label;
     private string _Option3Label;
     private string _Description;
    private int _CategoryID;

        public int ID { get => _ID; set => _ID = value; }
        public string QuestionText { get => _QuestionText; set => _QuestionText = value; }
        public bool IsOperationalRisk { get => _IsOperationalRisk; set => _IsOperationalRisk = value; }
        public string EgOption1 { get => _EgOption1; set => _EgOption1 = value; }
        public string EgOption2 { get => _EgOption2; set => _EgOption2 = value; }
        public string EgOption3 { get => _EgOption3; set => _EgOption3 = value; }

        public string[] EgOption1List { get { return OptionAsList(0); } }
        public string EgOption1Bullets { get => OptionsWithBullets(0); }
        public string[] EgOption2List { get { return OptionAsList(1); } }
        public string EgOption2Bullets { get => OptionsWithBullets(1); }
        public string[] EgOption3List { get { return OptionAsList(2); } }
        public string EgOption3Bullets { get => OptionsWithBullets(2); }

        private string[] OptionAsList(int num)
        {
            string str = null;
            switch (num)
            {
                case 0: str = EgOption1; break;
                case 1: str = EgOption2; break;
                case 2: str = EgOption3; break;
            }
            if (!string.IsNullOrEmpty(str))
            {
                string[] lst = str.Split('~');
                return lst;
            }
            else { return new string[0]; }
        }
        private string OptionsWithBullets(int num)
        {
            string str = null;
            switch (num)
            {
                case 0: str = EgOption1; break;
                case 1: str = EgOption2; break;
                case 2: str = EgOption3; break;
            }
            if (!string.IsNullOrEmpty(str))
            {
                return " • " + str.Replace("~", Environment.NewLine + " • ");
            }
            return null;
        }

        public string Option1Label { get => _Option1Label; set => _Option1Label = value; }
        public string Option2Label { get => _Option2Label; set => _Option2Label = value; }
        public string Option3Label { get => _Option3Label; set => _Option3Label = value; }
        public string Description { get => _Description; set => _Description = value; }
        public int CategoryID { get => _CategoryID; set => _CategoryID = value; }

        public RADeMSQuestion() { }
        public RADeMSQuestion(int id, int categoryid, string question, string desc, bool isOpRisk, string eg1, string eg2, string eg3)
        {
            _ID = id;
            _CategoryID = categoryid;
            _QuestionText = question;
            _Description = desc;
            _IsOperationalRisk = isOpRisk;
            EgOption1 = eg1;
            EgOption2 = eg2;
            EgOption3 = eg3;
            if (isOpRisk)
            {
                Option1Label = "Low";
                Option2Label = "Moderate";
                Option3Label = "High";
            }
            else
            {
                Option1Label = "High";
                Option2Label = "Moderate";
                Option3Label = "Low";

            }
        }
    }

    public static class RADeMSTools
    {


   
       
        public static List<RADeMSCategory> GetCategories()
        {
            List<RADeMSCategory> categories = new List<RADeMSCategory>();

            categories.Add(new RADeMSCategory(1, "Ground Search", GetQuestions(1)));
            categories.Add(new RADeMSCategory(2, "Human Disease Outbreak", GetQuestions(2)));
            categories.Add(new RADeMSCategory(3, "Motor Vehicle Operations", GetQuestions(3)));
            categories.Add(new RADeMSCategory(4, "Mountain Rescue", GetQuestions(4)));
            categories.Add(new RADeMSCategory(5, "Rope Rescue", GetQuestions(5)));
            categories.Add(new RADeMSCategory(6, "Swiftwater Rescue", GetQuestions(6)));


            return categories;
        }

        public static RADeMSCategory GetCategory(int id)
        {
            List<RADeMSCategory> categories = GetCategories();
            if (categories.Any(o => o.ID == id)) { return categories.First(o => o.ID == id); }
            else { return null; }
        }

        private static List<RADeMSQuestion> GetQuestions(int CategoryID)
        {
            List<RADeMSQuestion> questions = new List<RADeMSQuestion>();
            switch (CategoryID)
            {
                case 1: //gsar
                    questions.Add(new RADeMSQuestion(1, 1, "Operational complexity", "How complex & complicated is the task", true, "Simple operation, requiring one or two single activities (e.g. Locate and escort to safety)~1-8 hour duration~Minor subject injuries", "More complex operation, requiring multiple activities (e.g. locate and evacuate using a litter) with some challenges both in access and egress~8-24 hour duration~Significant non-life-threatening injuries", "Very complex operation involving multiple teams, convergent volunteers~Difficult access and egress~Helicopter transport~Multi-day or multi-organization response~Life threatening injuries"));
                    questions.Add(new RADeMSQuestion(2, 1, "Activity hazards", "How high are the hazards in the activities", true, "Simple basic GSAR activities; orienteering, tracking and/or sweep searching~Simple terrain~Hazards are low probability and low severity", "Potential for several disciplines required to access and evacuate the subject~Challenging terrain~Hazard probability high but severity manageable", "Repsonse may require use of several technical disciplines; rope rescue, swiftwater and/or avalanche assessment~Difficult terrain~Hazards are high probability and high severity"));
                    questions.Add(new RADeMSQuestion(3, 1, "Environmental conditions", "How high are the environmental hazards", true, "Minimal hazards~Good weather, stable forecast, daylight, warm temperatures and/or good visibilty~Low elevation", "Moderate hazards~Uncertain forecast, low light, freezing temperature, moderate precipitation and/or broken visibility", "Extreme hazards~Impending forecast, drastic weather system, darkness, cold temperatures, heavy precipitation, high wind and/or obscured visibility~High elevation"));
                    questions.Add(new RADeMSQuestion(4, 1, "Vulnerability", "How exposed and vulnerable are the team members", true, "Subject is able to walk out on their own with guidance from members keeping physical distancing", "Subject requires assistance to walk out, no first aid required", "Subject is incapacitated, requires assessment, first aid, evacuation and transportation"));
                    questions.Add(new RADeMSQuestion(5, 1, "External influence", "What is the level of pressure due to survivability, media, family, and/or other", true, "Little or no degree of urgency due to either confirmed survivability or confirmed deceased (recovery).~Little or no pressure from family, media or agency.", "Some degree of urgency due to subject survivability factors.~Local publicity.~Family on scene.~Agency and/or press asking questions.", "High degree of urgency due to critical subject survivability factors.~High profile subject.~Family on scene.~Agency, national media influence and/or political pressure to resolve."));
                    questions.Add(new RADeMSQuestion(6, 1, "Personnel Training", "What level of training do personnel have?", false, "Documented high training proficiency and practice in the use of PPE and other protective measures required under current directions", "Some training proficiency and practice in the use of PPE required under current directions", "Minimum instructions given on, and proficiency in, the use of PPE required under current directions "));
                    questions.Add(new RADeMSQuestion(7, 1, "Personnel Experience", "What level of expereience do personnel have", false, "Extensive experience appropriate for the activities required.~Routinely respond to incident site/area.~>20 similar responses~Many years of recreational experience.~Professional experience.", "Some experience related to the activities required. ~Some previous responses to site/area. ~5-20 similar responses.~Significant recreational experience.", "Little or no experience related to the activities required. ~Completely unfamiliar with the location. ~<5 similar responses. ~Little or no recreational experience."));
                    questions.Add(new RADeMSQuestion(8, 1, "Personnel Mental & Physical Preparedness", "How mentally and physically prepared are personnel", false, "Personnel are in good spirits, well-rested and exhibit characteristics of a cohesive team.", "Personnel are generally positive although some are tiring.~Adequate physical condition.", "Personnel are negative and question decisions (see note on page 13).~Resources have been on task through several assignments and are showing signs of exhaustion.~Searchers have been involved for multi-operational periods.~Unfit/does not exercise on a regular basis."));
                    questions.Add(new RADeMSQuestion(9, 1, "Planning", "How much planning has there been", false, "Plans (and/or Pre-plans) are in place, including contingencies, all documented.", "Overall plan is in place, with some consultation.~Basic notes taken about the site.", "Basic discussions on overall strategies and tactics.~Nothing in writing for this site."));
                    questions.Add(new RADeMSQuestion(10, 1, "Resources", "What is the level of resources available", false, "Appropriate resources are in place to cover all anticipated eventualities, contingencies and multi-operational periods.", "Basic resources are in place to cover response.~Back-up is available, but not on site.", "Resources are barely adequate.~No back-up or contingency available."));
                    break;
                case 2: //covid
                    questions.Add(new RADeMSQuestion(1, 2, "Operational complexity", "How complex & complicated is the task", true, "Simple operation, few members required to safely respond while maintaining reasonable physical distancing at the Incident Command Post and in the field", "More complex operation, additional resources required to safely respond but physical distancing is possible for the majority of activities except for transportation", "Very complex operation requiring large numbers of responders with technical rescue techniques requiring close contact between members and/or the subject"));
                    questions.Add(new RADeMSQuestion(2, 2, "Activity hazards", "How high are the hazards in the activities", true, "Travel with as few in vehicles where possible, no aircraft transportation required or few in aircraft if needed, wide area search with physical distancing being maintained", "Travel in vehicle and/or aircraft at capacity due to limited resources, technical rescue techniques requiring additional personnel, subject may have been exposed to pathogen", "Subject(s) require assessment, treatment, and/or transportation; Subject is known to be infected with the pathogen "));
                    questions.Add(new RADeMSQuestion(3, 2, "Environmental conditions", "How high are the environmental hazards", true, "Terrain allows for physical distancing weather is conducive to wearing PPE (dry, not excessively humid).", "Terrain requires responders to operate in close quarters with some use of hand holds or rope assist, weather is conducive for wearing gloves but challenging for wearing masks or other PPE for extended time", "Terrain requires using common hand holds, rope assist or ladders, and/or operating in close quarters.~Weather is wet or humid potentially rendering PPE ineffective (eg masks becoming soaked) or high temperatures increase risk of hyperthermia if wearing PPE"));
                    questions.Add(new RADeMSQuestion(4, 2, "Vulnerability", "How exposed and vulnerable are the team members", true, "Subject is able to walk out on their own with guidance from members keeping physical distancing", "Subject requires assistance to walk out, no first aid required", "Subject is incapacitated, requires assessment, first aid, evacuation and transportation"));
                    questions.Add(new RADeMSQuestion(5, 2, "External influence", "What is the level of pressure due to survivability, media, family, and/or other", true, "Little or no degree of urgency due to either confirmed survivability or confirmed deceased (recovery).~Little pressure from family, media or agency.~Protective measures (PPE and physical distancing) are clear and achievable.", "Some degree of urgency due to subject survivability factors.~Local publicity.~Family on scene.~Agency and/or press asking questions.~Recommended protective measures somewhat clear, but achievable", "High degree of urgency due to critical subject survivability factors.~High profile subject.~Family on scene.~Agency, provincial/national media influence and/or political pressure to resolve.~Recommended protective measures not clear."));
                    questions.Add(new RADeMSQuestion(6, 2, "Personnel Training", "What level of training do personnel have?", false, "Documented high training proficiency and practice in the use of PPE and other protective measures required under current directions", "Some training proficiency and practice in the use of PPE required under current directions", "Minimum instructions given on, and proficiency in, the use of PPE required under current directions"));
                    questions.Add(new RADeMSQuestion(7, 2, "Personnel Experience", "What level of expereience do personnel have", false, "Extensive experience in responding in a health threat environment", "Some experience in responding with others in a health threat environment and use of PPE", "No experience in responding with any aspect of a health threat environment or use of PPE"));
                    questions.Add(new RADeMSQuestion(8, 2, "Personnel Mental & Physical Preparedness", "How mentally and physically prepared are personnel", false, "Fit for service, not highly stressed over additional risk of contracting the pathogen above other hazards", "Fit for service, cautious over additional risk of contracting the pathogen, not sure if should be attending", "Not fit for service, overwhelmed by additional risk of contracting the pathogen or other factors"));
                    questions.Add(new RADeMSQuestion(9, 2, "Planning", "How much planning has there been", false, "Plans (and/or Pre-plans) are in place, including contingencies, all consistent with current protection requirements.", "Overall plan is in place, with consultation taking place over additional protective requirements", "Basic discussions on only overall strategies and tactics"));
                    questions.Add(new RADeMSQuestion(10, 2, "Resources", "What is the level of resources available", false, "Appropriate resources including required PPE are in place for known activities and any potential requirements as response develops", "Basic resources including required PPE are in place to cover known activities, with resources being available through mutual support if required during the response", "Resources including required PPE are barely adequate, will be overwhelmed if situation changes to any degree"));

                    break;
                case 3: //code 3
                    questions.Add(new RADeMSQuestion(1, 3, "Operational complexity", "How complex & complicated is the task", true, "Simple operation, driving light vehicle (e.g.~pickup/small rescue truck) with one other member assisting with communications and navigating.", "More complex operation, driving command vehicle with extended or wide body, or other vehicle with more than 1 other member as passengers or pulling small trailer (eg with PWCs, ORVs).", "Very complex operation involving driving vehicle with large trailer, eg command or communications trailer."));
                    questions.Add(new RADeMSQuestion(2, 3, "Activity hazards", "How high are the hazards in the activities", true, "Traffic light, few intersections, good visibility (minor hills, curves, etc) along roadways, roads conducive to traffic moving out of way if needed", "Traffic at medium capacity, flowing well, visibility okay, roads conducive to traffic moving out of way in some sections.", "Traffic heavy, visibility low, difficult for traffic to move out of way."));
                    questions.Add(new RADeMSQuestion(3, 3, "Environmental conditions", "How high are the environmental hazards", true, "Good weather, daylight, dry conditions, temperature well above freezing", "Variable weather, low light, lower but not freezing temperature, light precipitation.", "Poor weather (snow/heavy rain), darkness, freezing temperatures, high wind and/or obscured visibility.."));
                    questions.Add(new RADeMSQuestion(4, 3, "Vulnerability", "How exposed and vulnerable are the team members", true, "Short distance to incident, eg less than 5 km", "Medium distance to incident, eg 5 km to 10 km", "Long distance to incident, eg over 10 km"));
                    questions.Add(new RADeMSQuestion(5, 3, "External influence", "What is the level of pressure due to survivability, media, family, and/or other", true, "Little or no degree of urgency due to either confirmed survivability or confirmed deceased (recovery).~Little or no pressure from family, media or agency.", "Some degree of urgency due to subject survivability factors.~Local publicity.~Family on scene.~Agency and/or press asking questions.", "High degree of urgency due to critical subject survivability factors.~High profile subject.~Family on scene.~Agency, national media influence and/or political pressure to resolve."));
                    questions.Add(new RADeMSQuestion(6, 3, "Personnel Training", "What level of training do personnel have?", false, "Documented high training proficiency above than required by legislation.~Routinely train on vehicle and roadway being used.", "Some documented training proficiency above requirements.~Some training and practicing on vehicle and road being used, or similar vehicle and roadways.", "Minimum training required by legislation, little or no experience on type of vehicle and roadways to be used."));
                    questions.Add(new RADeMSQuestion(7, 3, "Personnel Experience", "What level of expereience do personnel have", false, "Extensive experience responding to incident site/area.~>20 similar responses.~Professional experience.", "Some previous responses to site/area.~5-20 similar responses.", "Unfamiliar with the location.~<5 similar responses."));
                    questions.Add(new RADeMSQuestion(8, 3, "Personnel Mental & Physical Preparedness", "How mentally and physically prepared are personnel", false, "Selected driver in good spirits, well-rested and focused on task", "Selected driver positive, coming off work or other activities.", "Approved drivers have been on task through several assignments or coming from strenuous/lengthy activities/work and are showing signs of exhaustion."));
                    questions.Add(new RADeMSQuestion(9, 3, "Planning", "How much planning has there been", false, "Plans (and/or Pre-plans) are in place, including contingencies, all documented.", "Overall plan is in place, with some consultation.", "Basic discussions on overall strategies and tactics.~Nothing in writing for this site."));
                    questions.Add(new RADeMSQuestion(10, 3, "Resources", "What is the level of resources available", false, "Appropriate resources are in place for other assignments letting approved driver focus on driving.", "Basic resources are in place to cover other assignments allowing approved driver to focus on driving.", "Resources are barely adequate, approved driver expected to manage other assignments"));

                    break;
                case 4: //mr
                    questions.Add(new RADeMSQuestion(1, 4, "Operational complexity", "How complex & complicated is the task", true, "Simple operation.~4th class terrain.~Trail approach.~Top-down access, requires only rappelling in.~Simple lower <50 meters in length.~Straight forward evacuation.~One subject.~Minor subject injuries.", "More complex operation.~5th class terrain.~Knot pass raise and/or lower <100 meters in length.~2 or more subjects with non-life-threatening injuries.", "Very complex operation.~Multiple teams.~Convergent volunteers.~Difficult access and egress.~Multi-pitch raise and/or lower >100 meters in length.~Complex evacuation.~Life-threatening injuries."));
                    questions.Add(new RADeMSQuestion(2, 4, "Activity hazards", "How high are the hazards in the activities", true, "Low hazard mountain rescue activities.~ Low angle alpine site.~No site hazards.~Simple terrain.~Hazards are low probability and low severity.", "Moderate hazard mountain rescue activities.~Glaciated alpine or steep alpine.~Challenging terrain.~Site hazards manageable.~Hazard probability high but severity manageable.", "High hazard mountain rescue.~Glaciated & steep alpine.~Avalanche, rock or icefall hazard likely, CDFL involved.~Complex terrain.~Site hazards not easily managed.~Hazards are high probability and high severity."));
                    questions.Add(new RADeMSQuestion(3, 4, "Environmental conditions", "How high are the environmental hazards", true, "Minimal hazards.~Good weather, stable forecast, daylight, warm temperatures and/or good visibility.~Low elevation.", "Moderate hazards.~Uncertain forecast, low light, freezing temperature, moderate precipitation and/or broken visibility.", "Extreme hazards.~Impending forecast, drastic weather system, darkness, cold temperatures, heavy precipitation, high wind and/or obscured visibility.~High elevation."));
                    questions.Add(new RADeMSQuestion(4, 4, "Vulnerability", "How exposed and vulnerable are the team members", true, "Minimal exposure to personnel.~Good terrain to work in.~No overhead hazards.~<1 hour exposure.~1-2 rescuers exposed.", "Moderate exposure to personnel due to terrain/activity/environmental hazards and/or weather.~Manageable overhead hazards.~Safe zones exist to protect rescuers.~1-4 hours exposure.~2-4 rescuers exposed.", "High exposure due to extreme terrain/activity/environmental hazards and/or weather.~High overhead hazards.~Rock fall likely.~No safe-zones to protect rescuers.~>4 hours exposure.~>4 rescuers exposed."));
                    questions.Add(new RADeMSQuestion(5, 4, "External influence", "What is the level of pressure due to survivability, media, family, and/or other", true, "Little or no degree of urgency due to either confirmed survivability or confirmed deceased (recovery).~Little or no pressure from family, media or agency.", "Some degree of urgency due to subject survivability factors.~Local publicity.~Family on scene.~Agency and/or press asking questions.", "High degree of urgency due to critical subject survivability factors.~High profile subject.~Family on scene.~Agency, national media influence and/or political pressure to resolve."));
                    questions.Add(new RADeMSQuestion(6, 4, "Personnel Training", "What level of training do personnel have?", false, "Documented training proficiency appropriate for the activities required.~Routinely train on incident site/area.~Professional level certification (ACMG Alpine Guide).~Instructor level mountain rescue certification.", "Some training proficiency related to the activities required.~Some training on incident site/area.~Mountain rescue training with team leader supervision.", "Little training proficiency related to the activities or location required.~No training on incident site/area.~No training in the terrain features.~Low mountain rescue certification.~New rescue team."));
                    questions.Add(new RADeMSQuestion(7, 4, "Personnel Experience", "What level of expereience do personnel have", false, "Extensive experience appropriate for the activities required.~Routinely respond on incident site/area.~>20 similar responses.~Many years of recreational experience.~Professional experience.", "Some experience related to the activities required.~Some previous responses on incident site/area.~5-20 similar responses.~Significant recreational experience.", "Little or no experience related to the activities required.~Completely unfamiliar with the location.~<5 similar responses.~Little or no recreational experience."));
                    questions.Add(new RADeMSQuestion(8, 4, "Personnel Mental & Physical Preparedness", "How mentally and physically prepared are personnel", false, "Personnel are in good spirits, well-rested and exhibit characteristics of a cohesive team.", "Personnel are generally positive although some are tiring.~Adequate physical condition.", "Personnel are negative and question decisions (see note on page 13).~Resources have been on task through several assignments and are showing signs of exhaustion.~Responders have been involved for multi-operational periods.~Unfit/does not exercise on a regular basis."));
                    questions.Add(new RADeMSQuestion(9, 4, "Planning", "How much planning has there been", false, "Plans (and/or Pre-plans) are in place, including contingencies, all documented.", "Overall plan is in place, with some consultation.~Basic notes taken.", "Basic discussions on overall strategies and tactics.~Nothing in writing."));
                    questions.Add(new RADeMSQuestion(10, 4, "Resources", "What is the level of resources available", false, "Appropriate resources and team are in place to cover all anticipated eventualities, contingencies and technical requirements.", "Basic resources and team are in place to cover response.~Back-up is available, but not on site.", "Resources and team are barely adequate.~No back-up or contingency available."));

                    break;
                case 5: //rope
                    questions.Add(new RADeMSQuestion(1, 5, "Operational complexity", "How complex & complicated is the task", true, "Simple operation.~4th class terrain.~Trail approach.~Top-down access.~Requires only rappelling in.~Simple lower <50 meters in length.~Straight forward evacuation.~One subject.~Minor subject injuries.", "More complex operation.~5th class terrain.~Knot pass raise and/or lower <100 meters in length.~2 or more subjects with non-life-threatening injuries.", "Very complex operation.~Multiple teams.~Convergent volunteers.~Difficult access and egress.~Multi-pitch raise and/or lower >100 meters in length.~Complex evacuation.~Life-threatening injuries."));
                    questions.Add(new RADeMSQuestion(2, 5, "Activity hazards", "How high are the hazards in the activities", true, "Simple operation.~4th class terrain.~Trail approach.~Top-down access.~Requires only rappelling in.~Simple lower <50 meters in length.~Straight forward evacuation.~One subject.~Minor subject injuries.", "More complex operation.~5th class terrain.~Knot pass raise and/or lower <100 meters in length.~2 or more subjects with non-life-threatening injuries.", "Very complex operation.~Multiple teams.~Convergent volunteers.~Difficult access and egress.~Multi-pitch raise and/or lower >100 meters in length.~Complex evacuation.~Life-threatening injuries."));
                    questions.Add(new RADeMSQuestion(3, 5, "Environmental conditions", "How high are the environmental hazards", true, "Minimal hazards.~Good weather, stable forecast, daylight, warm temperatures and/or good visibility.~Low elevation.", "Moderate hazards.~Uncertain forecast, low light, freezing temperature, moderate precipitation and/or broken visibility.", "Extreme hazards.~Impending forecast, drastic weather system, darkness, cold temperatures, heavy precipitation, high wind and/or obscured visibility.~ High elevation."));
                    questions.Add(new RADeMSQuestion(4, 5, "Vulnerability", "How exposed and vulnerable are the team members", true, "Minimal exposure to personnel.~Good terrain to work in.~No overhead hazards.~<1 hour exposure.~1-2 rescuers exposed.", "Moderate exposure to personnel due to terrain/activity/environmental hazards and/or weather.~Manageable overhead hazards.~Safe zones exist to protect rescuers.~Short-term exposure to hazards.~1-4 hours exposure.~2-4 rescuers exposed.", "High exposure due to extreme terrain/activity/environmental hazards and/or weather.~High overhead hazards.~Rock fall likely.~No safe-zones to protect rescuers.~Long-term exposure to hazards.~>4 hours exposure.~>4 rescuers exposed."));
                    questions.Add(new RADeMSQuestion(5, 5, "External influence", "What is the level of pressure due to survivability, media, family, and/or other", true, "Little or no degree of urgency due to either confirmed survivability or confirmed deceased (recovery).~Little or no pressure from family, media or agency.", "Some degree of urgency due to subject survivability factors.~Local publicity.~Family on scene.~Agency and/or press asking questions.", "High degree of urgency due to critical subject survivability factors.~High profile subject.~Family on scene.~Agency, national media influence and/or political pressure to resolve."));
                    questions.Add(new RADeMSQuestion(6, 5, "Personnel Training", "What level of training do personnel have?", false, "Documented training proficiency appropriate for the activities required.~Routinely train on incident site.~Rope rescue team member training with team leader supervision.", "Some training proficiency related to the activities required.~Some training on incident site.~Rope rescue team member training with team leader supervision.", "Little training proficiency related to the activities or location required.~No training on incident site.~No training in the terrain features.~Low rope rescue certification.~New rescue team."));
                    questions.Add(new RADeMSQuestion(7, 5, "Personnel Experience", "What level of expereience do personnel have", false, "Extensive experience appropriate for the activities required.~Routinely respond to incident site.~>20 similar responses.~Many years of recreational experience.~Professional experience.", "Some experience related to the activities required.~Some previous responses to incident site.~5-20 similar responses.~Significant recreational experience.", "Little or no experience related to the activities required.~Completely unfamiliar with the location.~<5 similar responses.~Little or no recreational experience."));
                    questions.Add(new RADeMSQuestion(8, 5, "Personnel Mental & Physical Preparedness", "How mentally and physically prepared are personnel", false, "Personnel are in good spirits, well-rested and exhibit characteristics of a cohesive team.~Excellent personal fitness.", "Personnel are generally positive although some are tiring.~Adequate physical condition.", "Personnel are negative and question decisions (see note on page 13).~Resources have been on task through several assignments and are showing signs of exhaustion.~Responders have been involved for multi-operational periods.~Unfit/does not exercise on a regular basis."));
                    questions.Add(new RADeMSQuestion(9, 5, "Planning", "How much planning has there been", false, "Plans (and/or Pre-plans) are in place, including contingencies, all documented.", "Overall plan is in place, with some consultation.~Basic notes taken about the site.", "Basic discussions on overall strategies and tactics.~Nothing in writing for this site."));
                    questions.Add(new RADeMSQuestion(10, 5, "Resources", "What is the level of resources available", false, "Appropriate resources are in place to cover all anticipated eventualities, contingencies and technical requirements.", "Basic resources are in place to cover response.~Back-up is available, but not on site.", "Resources are barely adequate.~No back-up or contingency available."));

                    break;
                case 6: //sw
                    questions.Add(new RADeMSQuestion(1, 6, "Operational complexity", "How complex & complicated is the task", true, "Simple operation.~Single tasks.~One subject.~Minor injuries.~Rescue from shore is possible.", "More complex operation.~Multiple single tasks.~Significant non-life-threatening injuries.~Rescuers in the water without anchored rope systems involved.~", "Very complex operation.~Compound tasks.~Life-threatening injuries.~Rescue includes anchored rope systems.~Complex mechanical advantage required."));
                    questions.Add(new RADeMSQuestion(2, 6, "Activity hazards", "How high are the hazards in the activities", true, "Low hazard swiftwater rescue activities.~Shore-based response in low risk environment.~Little or no direct contact with subject.~Indirect contact from shore.~Little to no site hazards.~Hazards are low probability and low severity.", "Moderate hazard swiftwater rescue activities.~Shore or water-based.~Simple and compound rope access.~Tethered swimmer in low-moderate risk swiftwater environments.~Site hazards manageable.~Hazard probability high but severity manageable.", "High hazard swiftwater rescue.~Shore and swim based.~Technical, horizontal aquatic rope access.~Complex rope, advanced rescue protocols.~Rescuers in the water.~Site hazards not easily managed.~Hazards are high probability and high severity."));
                    questions.Add(new RADeMSQuestion(3, 6, "Environmental conditions", "How high are the environmental hazards", true, "Minimal hazards.~Class 1.~Simple waves.~Pool drop.~Velocity <5 km/hr.~Drop <5m/km.~Sand/gravel bottom.~Water temp >20 C.~Water clarity > 2m.~Free flow hydraulics.~No hard hazards.~No entrapment potential.~Clear load.~Stable weather forecast.", "Moderate hazards.~Class 2-3.~Compound waves to 1m.~Staggered rapids.~Velocity 5-12 km/hr.~Drop 6-10m/km.~Boulder bottom.~Water temp 10-20 C.~Water clarity 0.3-1 m.~Potential holding hydraulics.~Some hard hazards.~Surface & suspended load.~Uncertain weather forecast, low light, freezing temperature.", "Extreme hazards.~Site hazards not easily managed.~Class 4+.~Complex waves 2m.~Continuous rapids.~Velocity 10 km+.~Boulder & ledge bottom.~Water temp <10 C.~Water clarity <0.3m.~Entrapment hydraulics.~Frequent hard hazards.~Surface/suspended & bottom load.~Impending weather forecast, darkness, cold temperatures, heavy precipitation, high wind."));
                    questions.Add(new RADeMSQuestion(4, 6, "Vulnerability", "How exposed and vulnerable are the team members", true, "Minimal exposure to personnel.~Good terrain to work in.~No downstream hazards.~No rescuers exposed to hazards.", "Moderate exposure to personnel due to terrain/activity/environmental hazards and/or weather.~Downstream hazards exist but are manageable.~Short-term exposure to hazards.~1 rescuer exposed to hazards.", "High exposure due to extreme terrain/activity/environmental hazards and/or weather.~Dangerous downstream hazards exist.~Medium to long-term exposure to hazards.~>1 rescuer exposed to hazards."));
                    questions.Add(new RADeMSQuestion(5, 6, "External influence", "What is the level of pressure due to survivability, media, family, and/or other", true, "Little or no degree of urgency due to either confirmed survivability or confirmed deceased (recovery).~Little or no pressure from family, media or agency.", "Some degree of urgency due to subject survivability factors.~Local publicity.~Family on scene.~Agency and/or press asking questions.", "High degree of urgency due to critical subject survivability factors.~High profile subject.~Family on scene.~Agency, national media influence and/or political pressure to resolve."));
                    questions.Add(new RADeMSQuestion(6, 6, "Personnel Training", "What level of training do personnel have?", false, "Documented training proficiency appropriate for the activities required.~Skill and knowledge to perform multiple complex specialized rescue tasks in high-risk swiftwater.~Routinely train on incident site/area.~Professional level rescuer certification.", "Some training proficiency related to the activities required.~Skill and knowledge to perform compound tasks in moderate swiftwater environments.~Some training on incident site/area.~All SRT certified rescuers.", "Little training proficiency related to the activities or location required.~Survival, awareness, or basic level swiftwater training only.~No training and/or experience on incident site/area.~Low SRT certification.~New SRT team."));
                    questions.Add(new RADeMSQuestion(7, 6, "Personnel Experience", "What level of expereience do personnel have", false, "Extensive experience related to the activities required.~Routinely respond on incident site/area.~>20 similar responses.~Many years of recreational experience.~Professional experience.", "Some experience related to the activities required.~Some previous response on incident site/area.~5-20 similar responses.~Significant recreational experience.", "Little or no experience related to the activities required.~Completely unfamiliar with the location.~<5 similar responses.~Little or no recreational experience."));
                    questions.Add(new RADeMSQuestion(8, 6, "Personnel Mental & Physical Preparedness", "How mentally and physically prepared are personnel", false, "Personnel are in good spirits, well-rested and exhibit characteristics of a cohesive team, excellent physical fitness.", "Personnel are generally positive although some are tiring.~Adequate physical condition.", "Personnel are negative and question decisions (see note on page 13).~Resources have been on task through several assignments and are showing signs of exhaustion.~Responders have been involved for multi-operational periods.~Unfit/does not exercise on a regular basis."));
                    questions.Add(new RADeMSQuestion(9, 6, "Planning", "How much planning has there been", false, "Plans (and/or Pre-plans) are in place, including contingencies, all documented.", "Overall plan is in place, with some consultation.~Basic notes taken.", "Basic discussions on overall strategies and tactics.~Nothing in writing."));
                    questions.Add(new RADeMSQuestion(10, 6, "Resources", "What is the level of resources available", false, "Appropriate resources and team are in place to cover all anticipated eventualities, contingencies and technical requirements.", "Basic resources and team are in place to cover response.~Back-up is available, but not on site.", "Resources and team are barely adequate.~No back-up or contingency available."));

                    break;

                default:
                    return null;
            }
            return questions;
        }
    }
}
