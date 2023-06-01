using demoD.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace demoD.Controllers
{
    public class EmpController : Controller
    {
        public List<EmpModel> LeaveList { get; set; }
        public EmpController()
        {
            LeaveList = new List<EmpModel>();          
        }

        // GET: Emp
        public ActionResult Index(EmpModel model)
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
        [HttpPost]
        public ActionResult Create(EmpModel model)
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["con"].ToString();
                using (MySqlConnection mycon = new MySqlConnection(con))
                {
                    string query = @"INSERT INTO demo.employee (EmpemployeName,StartDate,EndDate,LeaveType,Status,Description) VALUES (@EmpemployeName, @StartDate, @EndDate, @LeaveType, @Status, @Description)";
                    using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                    {
                        mycmd.Parameters.AddWithValue("@EmpemployeName", model.EmpemployeName);
                        mycmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                        mycmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                        mycmd.Parameters.AddWithValue("@LeaveType", model.LeaveType.ToString());
                        mycmd.Parameters.AddWithValue("@Status", model.Status);
                        mycmd.Parameters.AddWithValue("@Description", model.Description);
                        mycon.Open();
                        mycmd.ExecuteNonQuery();
                        mycon.Close();
                    }

                }
                ModelState.Clear();
            }
            catch
            {

            }   return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Message = "Data Insert Successfully";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmpModel result = new EmpModel();
            try
            {
                string con = ConfigurationManager.ConnectionStrings["con"].ToString();
                using (MySqlConnection mycon = new MySqlConnection(con))
                {
                    string query = @"SELECT * FROM demo.employee where Id = @Id";
                    using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                    {
                        mycmd.Parameters.AddWithValue("@Id", id);
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
                                    empModel.Status = "Pending";

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
                                    result = empModel;
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
            return View(result);
        }
        [HttpPost]
        public ActionResult Edit(EmpModel model)
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["con"].ToString();
                using (MySqlConnection mycon = new MySqlConnection(con))
                {
                    string query = @"Update demo.employee set EmpemployeName = @EmpemployeName,StartDate=@StartDate,EndDate=@EndDate,LeaveType=@LeaveType,Status=@Status,Description=@Description where Id = @Id";
                    using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                    {
                        mycmd.Parameters.AddWithValue("@EmpemployeName", model.EmpemployeName);
                        mycmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                        mycmd.Parameters.AddWithValue("@EndDate", model.EndDate);
                        mycmd.Parameters.AddWithValue("@LeaveType", model.LeaveType.ToString());
                        mycmd.Parameters.AddWithValue("@Status", model.Status);
                        mycmd.Parameters.AddWithValue("@Description", model.Description);
                        mycmd.Parameters.AddWithValue("@Id", model.Id);
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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                string con = ConfigurationManager.ConnectionStrings["con"].ToString();
                using (MySqlConnection mycon = new MySqlConnection(con))
                {
                    string query = @"DELETE FROM demo.employee  where Id = @Id";
                    using (MySqlCommand mycmd = new MySqlCommand(query, mycon))
                    {
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