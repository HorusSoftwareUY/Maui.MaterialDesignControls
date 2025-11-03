using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace HorusStudio.Maui.MaterialDesignControls.UITests.Tests;

public class TextFieldTests : BaseTest
{
    [Test]
    public void TextFieldTest()
    {
        ClickNavigationDrawerItem("menu_TextField");
        
        var txtFilled = FindUIElementById("txtFilled");
        txtFilled.Clear();
        txtFilled.SendKeys("Testing");
        Wait(500);
        Assert.That(txtFilled.Text, Is.EqualTo("Testing"));
        App.GetScreenshot().SaveAsFile($"{nameof(TextFieldTest)}.png");
        
        ClickTopAppBarLeadingIcon();
    }
}