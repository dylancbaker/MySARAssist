using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySARAssist.Interfaces;
using MySARAssist.Models;
using MySARAssist.ResourceClasses;
using Xamarin.Forms;

namespace MySARAssist.ViewModels
{
    class EditSavedTeamMemberViewModel : BaseViewModel
    {
        public EditSavedTeamMemberViewModel()
        {
            Organizations = OrganizationTools.GetOrganizations(Guid.Empty);
            CancelCommand = new Command(OnCancelCommand);
            SaveCommand = new Command(OnSaveCommand);
            NextCommand = new Command(OnNextCommand);
            DeleteCommand = new Command(OnDeleteCommand);
            BackCommand = new Command(OnBackCommand);
            CurrentMember = new Personnel();
            Organization mostPopularOrg = App.PersonnelManager.GetMostCommonOrganization();
            if (mostPopularOrg != null) { CurrentMember.MemberOrganization = mostPopularOrg; }
        }

        public List<Organization> Organizations { get; private set; }
        public Personnel CurrentMember { get; private set; }

        public Command CancelCommand { get; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        public Command NextCommand { get; }
        public Command BackCommand { get; }

        public Guid TeamMemberID
        {
            set
            {
                Guid ID = value;
                if (ID != Guid.Empty)
                {
                    SetTeamMember(ID);
                }
                else
                {
                    CurrentMember = new Personnel();
                    

                }
                OnPropertyChanged(nameof(CurrentMember));
            }
        }

        private async  void SetTeamMember(Guid ID)
        {
            CurrentMember  = await App.PersonnelManager.GetItemAsync(ID);
            if(CurrentMember.MemberOrganization == null && CurrentMember.OrganizationID != Guid.Empty)
            {
                CurrentMember.MemberOrganization = OrganizationTools.GetOrganization(CurrentMember.OrganizationID);
            }
            PersonQualifications = CurrentMember.GetPersonnelQualifications();
            if(CurrentMember != null && CurrentMember.MemberOrganization != null && OrganizationTools.GetParentOrganizations().Any(o=>o.OrganizationID == CurrentMember.MemberOrganization.ParentOrganizationID))
            {
                SelectedParentOrg = OrganizationTools.GetParentOrganizations().First(o => o.OrganizationID == CurrentMember.MemberOrganization.ParentOrganizationID);
                _selectedParentOrgID = SelectedParentOrg.OrganizationID;
            }
            
            OnPropertyChanged(nameof(PersonQualifications));
            OnPropertyChanged(nameof(SelectedParentOrg));

        }


        Guid _selectedParentOrgID = Guid.Empty;
        public Organization SelectedParentOrg
        {
           get
            {
                if (OrganizationTools.GetOrganization(_selectedParentOrgID) != null)
                {
                    return OrganizationTools.GetOrganization(_selectedParentOrgID);
                } else
                {
                    return OrganizationTools.GetParentOrganizations().First();
                }
            }
            set
            {
                _selectedParentOrgID = value.OrganizationID;
                Organizations = OrganizationTools.GetOrganizations(_selectedParentOrgID);
                OnPropertyChanged(nameof(Organizations));

            }
        }

        private List<Organization> _ParentOrgs = OrganizationTools.GetParentOrganizations();
        public List<Organization> ParentOrganizations { get => _ParentOrgs; }


        private async void OnCancelCommand()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async void OnSaveCommand()
        {
            ValidationResult validateResult = validateCurrent();
            if (!validateResult.success)
            {
                DependencyService.Get<Toast>().Show("ERROR: " + validateResult.message);

            }
            else
            {

                CurrentMember.Group = CurrentMember.MemberOrganization.OrganizationName;
                CurrentMember.OrganizationID = CurrentMember.MemberOrganization.OrganizationID;

                CurrentMember = removeBadChrs(CurrentMember);
                foreach(Qualification q in PersonQualifications)
                {
                    CurrentMember.QualificationList[q.QualificationListIndex] = q.PersonHas;
                }

                if (await App.PersonnelManager.UpsertItemAsync(CurrentMember))
                {
                    if (App.PersonnelManager.GetCurrentTeamMember() == null)
                    {
                        App.PersonnelManager.setCurrentTeamMember(CurrentMember.PersonID);
                        App.CurrentTeamMember = App.PersonnelManager.GetCurrentTeamMember();
                        OnPropertyChanged(nameof(App.CurrentTeamMember));
                    }

                    DependencyService.Get<Toast>().Show("Team Member Saved");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    DependencyService.Get<Toast>().Show("ERROR: Member not saved");
                }
            }
        }

        public bool ShowPersonnel { get; set; } = true;
        public bool ShowQualifications { get; set; }

        private  void OnNextCommand()
        {
            ValidationResult validateResult = validateCurrent();
            if (!validateResult.success)
            {
                DependencyService.Get<Toast>().Show("ERROR: " + validateResult.message);

            }
            else
            {
                //hide the individual stuff, show qualifications

                ShowPersonnel = false;
                OnPropertyChanged(nameof(ShowPersonnel));
                ShowQualifications = true;
                OnPropertyChanged(nameof(ShowQualifications));
            }
        }
        private  void OnBackCommand()
        {
            ShowPersonnel = true;
            OnPropertyChanged(nameof(ShowPersonnel));
            ShowQualifications = false;
            OnPropertyChanged(nameof(ShowQualifications));
        }

        private Personnel removeBadChrs(Personnel member)
        {
            member.Name = member.Name.removeBadChrsForQR();
            member.Callsign = member.Callsign.removeBadChrsForQR();
            member.Phone = member.Phone.removeBadChrsForQR();
            member.SpecialSkills = member.SpecialSkills.removeBadChrsForQR();
            member.Reference = member.Reference.removeBadChrsForQR();
            member.Barcode = member.Barcode.removeBadChrsForQR();
            member.Email = member.Email.removeBadChrsForQR();
            member.Pronouns = member.Pronouns.removeBadChrsForQR();
            member.Address = member.Address.removeBadChrsForQR();
            member.NOKName = member.NOKName.removeBadChrsForQR();
            member.NOKRelation = member.NOKRelation.removeBadChrsForQR();
            member.NOKPhone = member.NOKPhone.removeBadChrsForQR();
            member.D4HStatus = member.D4HStatus.removeBadChrsForQR();
            return member;
        }

        public List<Qualification> PersonQualifications { get; set; } 

        private ValidationResult validateCurrent()
        {
            ValidationResult result = new ValidationResult();
            StringBuilder msg = new StringBuilder();
            result.success = true;
            if (string.IsNullOrEmpty(CurrentMember.Name)) { result.success = false; msg.Append("You must include your full name."); }
            else if (!CurrentMember.Name.Contains(" ")) { result.success = false; msg.Append("You must include your full name."); }

            if(string.IsNullOrEmpty(CurrentMember.Email) || !CurrentMember.Email.isValidEmail()) { result.success = false; msg.Append("You must enter a valid email address."); }

            result.message = msg.ToString();
            return result;
        }

        private async void OnDeleteCommand()
        {
            if(await App.PersonnelManager.DeleteItemAsync(CurrentMember.PersonID))
            {
                if(App.CurrentTeamMember.PersonID == CurrentMember.PersonID)
                {
                    App.CurrentTeamMember = null;
                }
                DependencyService.Get<Toast>().Show("Team Member Deleted");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                DependencyService.Get<Toast>().Show("ERROR: Member not deleted");
            }
        }
    }
}
