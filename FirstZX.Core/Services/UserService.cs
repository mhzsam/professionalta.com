using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FirstZX.Core.Convertor;
using FirstZX.Core.DTO;
using FirstZX.Core.Generator;
using FirstZX.Core.Services.Interface;
using FirstZX.Datalayer.Context;
using FirstZX.Datalayer.Entities.User;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FirstZX.Core.Services
{
    public class UserService : IUserService
    {
        private FirstZXContext _context;

        public UserService(FirstZXContext context)
        {
            _context = context;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }

        public User LoginUser(LoginViewModel loginViewModel)
        {
            string hashPasword = PasswordGeneratorMD5.EncodePasswordMd5(loginViewModel.Password);
            string email = FixedText.FixEmail(loginViewModel.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPasword);
        }

        public User CheckUserEmailForReferral(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;
            else
            {
                user.IsActive = true;
                user.ActiveCode = CharGenerator.UnicCodeGenerate();
                user.IdentifireKey = CharGenerator.DigitalNumberGenerate();
                _context.SaveChanges();
                return true;
            }


        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activecode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activecode);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public UserInformationViewModel getUserInformation(string email)
        {
            var user = GetUserByEmail(email);
            UserInformationViewModel information = new UserInformationViewModel()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Country = user.Country,
                Email = user.Email,
                IdentifireEmail = user.IdentifireEmail,
                ValidId = user.ValidId,
                IdentifireKey = user.IdentifireKey,
                RegisterDate = user.RegisterDate,
                DigitalWalletId = user.DigitalWalletId,
                UserAvatar = user.UserAvatar,
                SelectedPlan = user.SelectedPlan
            };
            return information;
        }

        public SideBarUserPanelViewModel getSideBarUserPanelViewModel(string email)
        {
            var user = GetUserByEmail(email);
            SideBarUserPanelViewModel sideBarUserPanelinformation = new SideBarUserPanelViewModel()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                UserAvatar = user.UserAvatar,
                rank = user.Rank


            };
            return sideBarUserPanelinformation;
        }

        public EditeProfileViewModel getEditeProfileViewModel(string email)
        {
            var user = GetUserByEmail(email);
            EditeProfileViewModel editeProfileViewModel = new EditeProfileViewModel()
            {
                LastName = user.LastName,
                Country = user.Country,
                DigitalWalletId = user.DigitalWalletId,
                FirstName = user.FirstName,
                ValidId = user.ValidId,
                UserAvatar = user.UserAvatar



            };
            return editeProfileViewModel;
        }



        public bool EditeProfile(string email, EditeProfileViewModel editeProfile)
        {
            if (editeProfile.UserAvatarFormFile != null)
            {
                string imagePath = "";
                if (editeProfile.UserAvatar != "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar",
                        editeProfile.UserAvatar);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                editeProfile.UserAvatar = DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                                          Path.GetExtension(editeProfile.UserAvatarFormFile.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar",
                    editeProfile.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editeProfile.UserAvatarFormFile.CopyTo(stream);
                }
            }

            var user = GetUserByEmail(email);
            user.LastName = editeProfile.LastName;
            user.FirstName = editeProfile.FirstName;
            user.Country = editeProfile.Country;
            user.DigitalWalletId = editeProfile.DigitalWalletId;
            user.ValidId = editeProfile.ValidId;
            user.UserAvatar = editeProfile.UserAvatar;
            UpdateUser(user);
            return true;




        }

        public int GetUserIdByEmail(string email)
        {
            var userId = _context.Users.SingleOrDefault(s => s.Email == email).UserId;
            return userId;
        }

        public UsersForAdminViewModel GetUserForAdmin(int pageId = 1, string filterEmail = "", string filerUserId = "")
        {
            IQueryable<User> result = _context.Users;
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));

            }

            if (!string.IsNullOrEmpty(filerUserId))
            {
                result = result.Where(u => u.Email.Contains(filerUserId));
            }

            //Showitem in Page
            int take = 10;
            int skip = (pageId - 1) * take;


            UsersForAdminViewModel list = new UsersForAdminViewModel()
            {
                CurrentPage = pageId,
                PageCount = result.Count() / take,
                Users = result.OrderBy(u => u.UserId).Skip(skip).Take(take).ToList()

            };

            return list;

        }

        public void DeletUserForAdmin(string Email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == Email);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public void SaveEditeUserForEAdmin(EditeUserForAdminViewModel EditeUserForAdminViewModel)
        {
            if (EditeUserForAdminViewModel.UserAvatarFormFile != null)
            {
                string imagePath = "";
                if (EditeUserForAdminViewModel.UserAvatar != "Default.png")
                {
                    imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar",
                        EditeUserForAdminViewModel.UserAvatar);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }

                EditeUserForAdminViewModel.UserAvatar = DateTime.Now.ToString("yyyyMMddHHmmssfff") +
                                                        Path.GetExtension(EditeUserForAdminViewModel.UserAvatarFormFile
                                                            .FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar",
                    EditeUserForAdminViewModel.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    EditeUserForAdminViewModel.UserAvatarFormFile.CopyTo(stream);
                }
            }



            var user = GetUserByEmail(EditeUserForAdminViewModel.Email);
            user.LastName = EditeUserForAdminViewModel.LastName;
            user.FirstName = EditeUserForAdminViewModel.FirstName;
            user.Country = EditeUserForAdminViewModel.Country;
            user.DigitalWalletId = EditeUserForAdminViewModel.DigitalWalletId;
            user.ValidId = EditeUserForAdminViewModel.ValidId;
            user.UserAvatar = EditeUserForAdminViewModel.UserAvatar;
            user.DigitalWalletId = EditeUserForAdminViewModel.DigitalWalletId;
            user.IdentifireEmail = EditeUserForAdminViewModel.IdentifireEmail;
            user.IsActive = EditeUserForAdminViewModel.IsActive;
            user.IdentifireKey = EditeUserForAdminViewModel.IdentifireKey;
            user.RegisterDate = EditeUserForAdminViewModel.RegisterDate;
            user.Rank = EditeUserForAdminViewModel.Rank;
            user.SelectedPlan = EditeUserForAdminViewModel.SelectedPlan;

            UpdateUser(user);
        }


        public List<EditeBankForAdminViewModel> GetbankDetailForAdmin(int id)
        {
            List<int> bankId = _context.BankMonths.Where(u => u.UserId == id).Select(s => s.BankMonthId).ToList();
            var model = new List<EditeBankForAdminViewModel>();
            foreach (var item in bankId)
            {
                model.Add(new EditeBankForAdminViewModel()
                {
                    UserId = id,
                    BankMonthId = item,
                    Binary = _context.BankMonths.Single(s => s.BankMonthId == item).Binary,
                    DirectSell = _context.BankMonths.Single(s => s.BankMonthId == item).DirectSell,
                    InDirect = _context.BankMonths.Single(s => s.BankMonthId == item).InDirect,
                    Matrix = _context.BankMonths.Single(s => s.BankMonthId == item).Matrix,
                    Saving = _context.BankMonths.Single(s => s.BankMonthId == item).Saving,
                    UniLevel = _context.BankMonths.Single(s => s.BankMonthId == item).UniLevel,
                    InvestMonth = _context.BankMonths.Single(s => s.BankMonthId == item).InvestMonth

                });

            }

            return model;
        }

        public List<EditeBankForAdminViewModelWeek> GetbankDetailForAdminWeek(int id)
        {
            List<int> bankId = _context.BankWeeks.Where(u => u.UserId == id).Select(s => s.BankWeekId).ToList();
            var model = new List<EditeBankForAdminViewModelWeek>();
            foreach (var item in bankId)
            {
                model.Add(new EditeBankForAdminViewModelWeek()
                {
                    UserId = id,
                    BankWeekId = item,
                    Binary = _context.BankWeeks.Single(s => s.BankWeekId == item).Binary,
                    DirectSell = _context.BankWeeks.Single(s => s.BankWeekId == item).DirectSell,
                    InDirect = _context.BankWeeks.Single(s => s.BankWeekId == item).InDirect,
                    Matrix = _context.BankWeeks.Single(s => s.BankWeekId == item).Matrix,
                    Saving = _context.BankWeeks.Single(s => s.BankWeekId == item).Saving,
                    UniLevel = _context.BankWeeks.Single(s => s.BankWeekId == item).UniLevel,
                    InvestWeek = _context.BankWeeks.Single(s => s.BankWeekId == item).InvestWeek

                });

            }

            return model;
        }

        public void AddRowToBankUser(int id)
        {
            int userId = id;
            var newRaw = new BankMonth()
            {
                UserId = userId
            };
            _context.BankMonths.Add(newRaw);
            _context.SaveChanges();
        }

        public void AddRowToBankUserWeek(int id)
        {
            int userId = id;
            var newRaw = new BankWeek()
            {
                UserId = userId
            };
            _context.BankWeeks.Add(newRaw);
            _context.SaveChanges();
        }

        public DeleteRowForAdmin FinddeleteRowForAdmin(int bankId)
        {
            var bankRow = _context.BankMonths.Find(bankId);
            DeleteRowForAdmin DeleteRowForAdmin = new DeleteRowForAdmin()
            {
                UserId = bankRow.UserId,
                DirectSell = bankRow.DirectSell,
                UniLevel = bankRow.UniLevel,
                Matrix = bankRow.Matrix,
                InDirect = bankRow.InDirect,
                Binary = bankRow.Binary,
                Saving =bankRow.Saving,
                BankMonthId = bankRow.BankMonthId,
                InvestMonth = bankRow.InvestMonth
            };
            return DeleteRowForAdmin;
        }

        public DeleteRowForAdminWeek FinddeleteRowForAdminWeek(int bankId)
        {
            var bankRow = _context.BankWeeks.Find(bankId);
            DeleteRowForAdminWeek DeleteRowForAdminWeek = new DeleteRowForAdminWeek()
            {
                UserId = bankRow.UserId,
                DirectSell = bankRow.DirectSell,
                UniLevel = bankRow.UniLevel,
                Matrix = bankRow.Matrix,
                InDirect = bankRow.InDirect,
                Binary = bankRow.Binary,
                Saving = bankRow.Saving,
                BankWeekId = bankRow.BankWeekId,
                InvestWeek = bankRow.InvestWeek
            };
            return DeleteRowForAdminWeek;
        }

        public void DeleteBankRowForAdmin(int bankId)
        {
            var bankRow = _context.BankMonths.Find(bankId);
            _context.Remove(bankRow);
            _context.SaveChanges();
        }

        public void DeleteBankRowForAdminWeek(int bankId)
        {
            var bankRow = _context.BankWeeks.Find(bankId);
            _context.Remove(bankRow);
            _context.SaveChanges();
        }

        public void SaveEditedRowForAdmin(List<EditeBankForAdminViewModel> models)
        {


            foreach (var item in models)
            {
                var bankModel = new BankMonth()
                {
                    BankMonthId = item.BankMonthId,
                    DirectSell = item.DirectSell,
                    UniLevel = item.UniLevel,
                    Matrix = item.Matrix,
                    InvestMonth = item.InvestMonth,
                    InDirect = item.InDirect,
                    Binary = item.Binary,
                    Saving = item.Saving,
                    UserId = item.UserId
                };
                _context.BankMonths.Update(bankModel);
            }

            _context.SaveChanges();

        }

        

        public void SaveEditedRowForAdminWeek(List<EditeBankForAdminViewModelWeek> models)
        {


            foreach (var item in models)
            {
                var bankModel = new BankWeek()
                {
                    BankWeekId = item.BankWeekId,
                    DirectSell = item.DirectSell,
                    UniLevel = item.UniLevel,
                    Matrix = item.Matrix,
                    InvestWeek = item.InvestWeek,
                    InDirect = item.InDirect,
                    Binary = item.Binary,
                    Saving = item.Saving,
                    UserId = item.UserId
                };
                _context.BankWeeks.Update(bankModel);
            }

            _context.SaveChanges();
        }

        public void SaveEarnBankForAdmin(BankDetailEarn models)
        {
            _context.BankDetailEarn.Update(models);
            _context.SaveChanges();
        }

        public void SaveEarnBankForAdminWeek(BankDetailEarnWeek models)
        {
            _context.BankDetailEarnWeek.Update(models);
            _context.SaveChanges();
        }

        public List<BankMonth> GetBankDetailForUser(string email)
        {
            int userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;
            List<BankMonth> banlDetail = _context.BankMonths.Where(b => b.UserId == userId).ToList();
            return banlDetail;
        }

        public List<BankWeek> GetBankDetailForUserWeek(string email)
        {
            int userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;
            List<BankWeek> banlDetail = _context.BankWeeks.Where(b => b.UserId == userId).ToList();
            return banlDetail;
        }

        public BankRequest getBankRequest(string email)
        {
            int userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;
            BankRequest bankRequest = _context.BankRequests.Where(s => s.IsDeActive == false).SingleOrDefault(b => b.UserId == userId);
            return bankRequest;
        }

        public BankDetailEarn getBankDetailEarn(string email)
        {
            int userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;
            BankDetailEarn bankDetailEarn = _context.BankDetailEarn.SingleOrDefault(b => b.UserId == userId);
            return bankDetailEarn;
        }

        public BankDetailEarnWeek getBankDetailEarnWeek(string email)
        {
            int userId = _context.Users.SingleOrDefault(u => u.Email == email).UserId;
            BankDetailEarnWeek bankDetailEarnWeek = _context.BankDetailEarnWeek.SingleOrDefault(b => b.UserId == userId);
            return bankDetailEarnWeek;
        }

        public void SubmitUserReinvest(int userId)
        {
            var userIdFind = _context.BankRequests.Where(b => b.IsDeActive == false).SingleOrDefault(s => s.UserId == userId);
            if (userIdFind == null && userIdFind == default)
            {
                var email = _context.Users.SingleOrDefault(s => s.UserId == userId).Email;
                var bankRequest = new BankRequest()
                {
                    UserId = userId,
                    CashOut = false,
                    ConfirmCashout = false,
                    Reinvest = true,
                    ConfirmReinvest = false,
                    RequestTime = DateTime.Now,
                    Email = email
                };
                _context.BankRequests.Add(bankRequest);
                _context.SaveChanges();
            }

            if (userIdFind != null)
            {
                BankRequest bankRequest = _context.BankRequests.Where(s => s.IsDeActive == false).Single(s => s.UserId == userId);
                if (bankRequest.CashOut == true && bankRequest.ConfirmCashout == false)
                {
                    bankRequest.CashOut = false;
                    bankRequest.Reinvest = true;
                    _context.BankRequests.Update(bankRequest);
                    _context.SaveChanges();
                }
            }


        }

        public void SubmitUserCasOut(int userId)
        {
            var userIdFind = _context.BankRequests.Where(s => s.IsDeActive == false).SingleOrDefault(s => s.UserId == userId);
            if (userIdFind == null && userIdFind == default)
            {
                var email = _context.Users.SingleOrDefault(s => s.UserId == userId).Email;
                var bankRequest = new BankRequest()
                {
                    UserId = userId,
                    CashOut = true,
                    ConfirmCashout = false,
                    Reinvest = false,
                    ConfirmReinvest = false,
                    RequestTime = DateTime.Now,
                    Email = email
                };
                _context.BankRequests.Add(bankRequest);
                _context.SaveChanges();
            }
            if (userIdFind != null)
            {
                BankRequest bankRequest = _context.BankRequests.Where(s => s.IsDeActive == false).Single(s => s.UserId == userId);
                if (bankRequest.Reinvest == true && bankRequest.ConfirmReinvest == false)
                {
                    bankRequest.Reinvest = false;
                    bankRequest.CashOut = true;
                    _context.BankRequests.Update(bankRequest);
                    _context.SaveChanges();
                }
            }
        }

        public BankDetailEarn getBankDetailEarn(int id)
        {
            return (_context.BankDetailEarn.SingleOrDefault(s => s.UserId == id));
        }

        public BankDetailEarnWeek getBankDetailEarnWeek(int id)
        {
            return (_context.BankDetailEarnWeek.SingleOrDefault(s => s.UserId == id));
        }

        public void CreateBankDetailEarnRow(int id)
        {
            BankDetailEarn bankDetailEarn = new BankDetailEarn()
            {
                UserId = id,
                RewardEarnmonth1 = 0,
                RewardEarnmonth2 = 0,
                RewardEarnmonth3 = 0,
                RewardEarnmonth4 = 0
            };
            _context.BankDetailEarn.Update(bankDetailEarn);
            _context.SaveChanges();
        }

        public void CreateBankDetailEarnRowWeek(int id)
        {
            BankDetailEarnWeek bankDetailEarnWeek = new BankDetailEarnWeek()
            {
                UserId = id,
                RewardEarnWeek1 = 0,
                RewardEarnWeek2 = 0,
                RewardEarnWeek3 = 0,
                RewardEarnWeek4 = 0

            };
            _context.BankDetailEarnWeek.Update(bankDetailEarnWeek);
            _context.SaveChanges();
        }


        public List<UserAnswer> getUserAnswers(string email)
        {
            return _context.UserAnswer.Where(u => u.Email == email && u.IsDelete == false).ToList();
        }

        public void StartForeCast(int userId)
        {
            string email = _context.Users.SingleOrDefault(u => u.UserId == userId).Email;
            UserAnswer userAnswer = new UserAnswer()
            {
                UserId = userId,
                Email = email,
                DateTime = DateTime.Now,
                IsDeactive = false
            };
            _context.UserAnswer.Add(userAnswer);
            _context.SaveChanges();

        }

        public List<Answer> getAnswers(int UAId)
        {
            var result = _context.Answers.FirstOrDefault(u => u.UserAnswerId == UAId);
            if (result == null)
            {
                for (int i = 0; i < 3; i++)
                {
                    Answer answer = new Answer()
                    {
                        UserAnswerId = UAId,
                        Cryptocurrency = "",
                        BuyOrSell = 1,
                        Percentage = 0,
                        RatioCryptocurrency = "",



                    };
                    _context.Answers.Add(answer);

                }

                _context.SaveChanges();
            }

            return _context.Answers.Where(u => u.UserAnswerId == UAId).ToList();


        }

        public bool SaveForcastAnswer(List<Answer> answers)
        {

            for (int i = 0; i < answers.Count; i++)
            {
                _context.Answers.Update(answers[i]);
            }

            var UAId = answers[0].UserAnswerId;
            _context.UserAnswer.SingleOrDefault(u => u.UserAnswerId == UAId).IsDeactive = true;
            _context.SaveChanges();
            return true;


        }

        public bool CheckUAIdIsDeactive(int uaId)
        {
            var UAIdIsDeactive = _context.UserAnswer.SingleOrDefault(u => u.UserAnswerId == uaId).IsDeactive;
            if (UAIdIsDeactive == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool NextLevelForcastChecker(int UAId)
        {
            var FindLastForcast = _context.UserAnswer.Where(u => u.UserId == UAId).OrderBy(o => o.DateTime)
                .Select(s => s.DateTime).Last();
            var LastForcast = (FindLastForcast.Year.ToString() + FindLastForcast.Month.ToString() + FindLastForcast.Day.ToString());
            var NewForcast = ((DateTime.Now).Year.ToString() + (DateTime.Now).Month.ToString() + (DateTime.Now).Day).ToString();
            var LastForcastint = Int32.Parse(LastForcast);
            var NewForcastint = Int32.Parse(NewForcast);
            if (LastForcastint == NewForcastint || LastForcastint >= NewForcastint)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public GetUserForecastForAdninViewModel GetUserForecastForAdnin(int pageId = 1, string filterEmail = "", string filterUserId = "")
        {
            IQueryable<UserAnswer> result = _context.UserAnswer.Where(u => u.IsDelete == false);
            if (!string.IsNullOrEmpty(filterEmail))
            {
                result = result.Where(u => u.Email.Contains(filterEmail));

            }

            //Showitem in Page
            int take = 10;
            int skip = (pageId - 1) * take;


            GetUserForecastForAdninViewModel list = new GetUserForecastForAdninViewModel()
            {
                CurrentPage = pageId,
                PageCount = result.Count() / take,
                UserAnswers = result.OrderByDescending(u => u.DateTime).Skip(skip).Take(take).ToList()

            };

            return list;
        }

        public List<Answer> getUserAnswersForAdmin(int UAId)
        {
            return _context.Answers.Where(a => a.UserAnswerId == UAId).ToList();
        }

        public void RestartUserForcast(int UAId)
        {
            var UserId = _context.UserAnswer.Where(u => u.UserAnswerId == UAId).Select(u => u.UserId).FirstOrDefault();
            var userAnswers = _context.UserAnswer.Where(u => u.UserId == UserId).ToList();
            foreach (var item in userAnswers)
            {
                item.IsDelete = true;
            }
            _context.UpdateRange(userAnswers);
            _context.SaveChanges();
        }

    }
}