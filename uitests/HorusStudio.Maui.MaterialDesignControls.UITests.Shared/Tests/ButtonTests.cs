using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace HorusStudio.Maui.MaterialDesignControls.UITests.Tests;

public class ButtonTests : BaseTest
{
    [Test]
    public void ButtonTest()
    {
	    ClickNavigationDrawerItem("menu_Button");
	    
	    ScrollToElement("ClickBtn");
	    var clickBtn = FindUIElementById("ClickBtn");
	    Assert.That(clickBtn.Text, Is.EqualTo("Click me"));
	    clickBtn.Click();
	    Wait(500);
	    Assert.That(clickBtn.Text, Is.EqualTo("Clicked 1 time"));
	    App.GetScreenshot().SaveAsFile($"{nameof(ButtonTest)}.png");

	    ClickTopAppBarLeadingIcon();
    }
}