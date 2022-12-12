﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IProject
    {
        List<ProjectMaster> GetListofProjects(int userid);
       // List<ProjectMaster> GetListofProjects();
        bool CheckProjectCodeExists(string ProjectCode);

        //Created By Prasanth 
        //EditProject Details
        ProjectMaster GetProjectDetailsByProjectID(int? ProjectID);
        bool CheckProjectNameExists(string ProjectName);
        int SaveProject(ProjectMaster ProjectMaster);
        IQueryable<ProjectMaster> ShowProjects(string sortColumn, string sortColumnDir, string Search);
        bool CheckProjectIDExistsInTimesheet(int ProjectID);
        bool CheckProjectIDExistsInExpense(int ProjectID);
        int ProjectDelete(int ProjectID);
        int GetTotalProjectsCounts();
    }
}
