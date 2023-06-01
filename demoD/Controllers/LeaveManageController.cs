using demoD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demoD.Controllers
{
    public class LeaveManageController : Controller
    {
        public List<EmpModel> LeaveList { get; set; }
        public LeaveManageController()
        {
            LeaveList = new List<EmpModel>();
        }
        // GET: LeaveManage
        public ActionResult Index()
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["con"].ToString();
                using (MySqlConnection mycon = new MySqlConnection(con))
                {
                    string query = @"SELECT * FROM demo.employee";
                    using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                    {
                        MySqlDataReader myReader;
                        mycon.Open();
                        using (myReader = mycmd.ExecuteReader())
                        {
                            if (myReader.HasRows)
                            {
                                while (myReader.Read())
                                {
                                    EmpModel empModel = new EmpModel();
                                    empModel.Id = Convert.ToInt32(myReader["Id"]);
                                    empModel.EmpemployeName = myReader["EmpemployeName"].ToString();
                                    empModel.StartDate = Convert.ToDateTime(myReader["StartDate"]);
                                    empModel.EndDate = Convert.ToDateTime(myReader["EndDate"]);
                                    empModel.Status = myReader["Status"].ToString();
                                    if (myReader["LeaveType"].ToString() == LeaveType.CL.ToString())
                                    {
                                        empModel.LeaveType = LeaveType.CL;
                                    }
                                    else if (myReader["LeaveType"].ToString() == LeaveType.LWP.ToString())
                                    {
                                        empModel.LeaveType = LeaveType.LWP;
                                    }
                                    else if (empModel.LeaveType == LeaveType.HL)
                                    {
                                        empModel.LeaveType = LeaveType.HL;
                                    }
                                    empModel.Description = myReader["Description"].ToString();
                                    LeaveList.Add(empModel);
                                }
                            }
                        }
                        mycon.Close();
                    }
                }
            }
            catch
            {

            }
            return View(LeaveList);
        }
        public ActionResult UpdateStatus(int id, bool status)
        {            
            try
            {
                string con = ConfigurationManager.ConnectionStrings["con"].ToString();
                using (MySqlConnection mycon = new MySqlConnection(con))
                {
                    string query = @"Update demo.employee set Status=@Status  where Id = @Id";
                    using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                    {
                        mycmd.Parameters.AddWithValue("@Status", status ? "Approved": "Rejected");                        
                        mycmd.Parameters.AddWithValue("@Id", id);
                        mycon.Open();
                        mycmd.ExecuteNonQuery();
                        mycon.Close();
                    }

                }
                ModelState.Clear();
            }
            catch
            {

            }
            return RedirectToAction("index");
        }
      
    }

}