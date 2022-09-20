using CuriositySoftware.RunResult.Entities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Tests.Utilities
{
    public class TestModellerLogger
    {
        public static List<TestPathRunStep> steps = new List<TestPathRunStep>();

        public static String LastNodeGuid;

        public static void LogMessage(String name, String description, TestPathRunStatus status)
        {
            TestPathRunStep step = new TestPathRunStep();

            step.stepName = (name);
            step.stepDescription = (description);
            step.testStatus = (status);
            step.nodeGuid = (LastNodeGuid);

            steps.Add(step);
        }

        public static void LogMessageWithScreenshot(String name, String description, byte[] image, TestPathRunStatus status)
        {
            TestPathRunStep step = new TestPathRunStep();

            step.stepName = (name);
            step.stepDescription = (description);
            step.image = (image);
            step.testStatus = (status);
            step.nodeGuid = (LastNodeGuid);

            steps.Add(step);
        }

        public static void FailStep(String stepName, String details)
        {
            TestPathRunStep step = new TestPathRunStep();

            step.stepName = (stepName);
            step.stepDescription = (details);
            step.testStatus = (TestPathRunStatus.Failed);
            step.nodeGuid = (LastNodeGuid);

            steps.Add(step);
        }

        public static void PassStep(IWebDriver driver, String stepName)
        {
            TestPathRunStep step = new TestPathRunStep();

            step.stepName = (stepName);
            step.testStatus = (TestPathRunStatus.Passed);
            step.nodeGuid = (LastNodeGuid);

            steps.Add(step);
        }

        public static void PassStepWithScreenshot(IWebDriver driver, String stepName)
        {
            PassStepWithScreenshot(driver, stepName, stepName);
        }

        public static void PassStepWithScreenshot(IWebDriver driver, String stepName, String details)
        {
            TestPathRunStep step = new TestPathRunStep();

            step.stepName = (stepName);
            step.stepDescription = (details);
            step.image = (GetScreenShot.captureAsByteArray(driver));
            step.testStatus = (TestPathRunStatus.Passed);
            step.nodeGuid = (LastNodeGuid);

            steps.Add(step);
        }

        public static void FailStepWithScreenshot(IWebDriver driver, String stepName)
        {
            FailStepWithScreenshot(driver, stepName, stepName);
        }

        public static void FailStepWithScreenshot(IWebDriver driver, String stepName, String details)
        {
            TestPathRunStep step = new TestPathRunStep();

            step.stepName = (stepName);
            step.stepDescription = (details);
            step.image = (GetScreenShot.captureAsByteArray(driver));
            step.testStatus = (TestPathRunStatus.Failed);
            step.nodeGuid = (LastNodeGuid);

            steps.Add(step);
        }

        public static void SetLastNodeGuid(String guid)
        {
            LastNodeGuid = guid;
        }

        public static void ClearMessages()
        {
            steps.Clear();
        }

    }
}
