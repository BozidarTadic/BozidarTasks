using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasks.BL;
using Tasks.BL.Interfaces;
using Tasks.Data.Data;

namespace Tasks.Tests.Tasts
{
    [TestClass]
    class TasksServiceTests
    {
        readonly Mock<ITaskService> _taskService;
        public TasksServiceTests(Mock<ITaskService>  taskService)
        {
            _taskService = taskService;
            
        }
        Task tasks = new Task
        {
            Id = 0,
            PriorityId = 0,
            Desription = "desc",
            Name = "name",
            ProjectId = 0,
            StatusId = 0,

        };

        [TestMethod]
        public void GetTask_Successful()
        {
            //asert
            
            _taskService.Setup()
            //act 



        }
    }
}
