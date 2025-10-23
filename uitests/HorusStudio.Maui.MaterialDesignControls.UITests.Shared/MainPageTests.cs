using NUnit.Framework;

namespace HorusStudio.Maui.MaterialDesignControls.UITests;

public class MainPageTests : BaseTest
{
	[Test]
	public void AppLaunches()
	{
		App.GetScreenshot().SaveAsFile($"{nameof(AppLaunches)}.png");
	}

	[Test]
	public void ClickCounterTest()
	{
		// Arrange
		// Find elements with the value of the AutomationId property
		var element = FindUIElement("btnSample");

		// Act
		element.Click();
		Task.Delay(500).Wait(); // Wait for the click to register and show up on the screenshot

		// Assert
		App.GetScreenshot().SaveAsFile($"{nameof(ClickCounterTest)}.png");
		Assert.That(element.Text, Is.EqualTo("Clicked!"));
	}
}