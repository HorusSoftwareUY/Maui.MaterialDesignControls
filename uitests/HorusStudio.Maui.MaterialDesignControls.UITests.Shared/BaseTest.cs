using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using HorusStudio.Maui.MaterialDesignControls.UITests.Utils;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace HorusStudio.Maui.MaterialDesignControls.UITests;

public abstract class BaseTest
{
	protected AppiumDriver App => AppiumSetup.App;
	
	protected AppiumElement? FindUIElementById(string id)
	{
		try
		{
			if (App is WindowsDriver)
			{
				return App.FindElement(MobileBy.AccessibilityId(id));
			}
			
			return App.FindElement(MobileBy.Id(id));
		}
		catch (NoSuchElementException ex)
		{
			Logger.LogInfo($"{nameof(FindUIElementById)} - Id: {id} - {nameof(NoSuchElementException)}: {ex.Message}");

			return FindUIElementByContentDescription(id);
		}
	}
	
	private AppiumElement? FindUIElementByContentDescription(string contentDescription)
	{
		try
		{
			return App.FindElement(MobileBy.XPath($"//*[@content-desc='{contentDescription}']"));
		}
		catch (NoSuchElementException ex)
		{
			Logger.LogInfo($"{nameof(FindUIElementByContentDescription)} - ContentDescription: {contentDescription} - {nameof(NoSuchElementException)}: {ex.Message}");
			return null;
		}
	}
	
	public bool ScrollToElement(string id)
	{
		var maxScrolls = 10;
		
		for (int i = 0; i < maxScrolls; i++)
		{
			try
			{
				AppiumElement element = null;
				
				if (App is WindowsDriver)
				{
					element = App.FindElement(MobileBy.AccessibilityId(id));
				}
				else
				{
					element = App.FindElement(MobileBy.Id(id));
				}
				
				if (element != null)
				{
					return true;
				}
			}
			catch (NoSuchElementException)
			{
				var actions = new PointerInputDevice(PointerKind.Touch);
				var sequence = new ActionSequence(actions, 0);
				sequence.AddAction(actions.CreatePointerMove(CoordinateOrigin.Viewport, 540, 1872, TimeSpan.Zero));
				sequence.AddAction(actions.CreatePointerDown(MouseButton.Touch));
				sequence.AddAction(actions.CreatePause(TimeSpan.FromMilliseconds(500)));
				sequence.AddAction(actions.CreatePointerMove(CoordinateOrigin.Viewport, 540, 468, TimeSpan.FromMilliseconds(1000)));
				sequence.AddAction(actions.CreatePointerUp(MouseButton.Touch));
				App.PerformActions(new List<ActionSequence> { sequence });
			}
		}

		return false;
	}

	protected void Wait(int milliseconds)
	{
		Task.Delay(milliseconds).Wait();
	}

	protected void ClickNavigationDrawerItem(string itemAutomationId)
	{
		var menu = FindUIElementById("menu");
		Assert.That(menu, Is.Not.Null, "MaterialIconButton with id 'menu' was not found.");
		menu.Click();
		Wait(2000);
	    
		ScrollToElement(itemAutomationId);
		Wait(500);
		
		var item = FindUIElementById(itemAutomationId);
		Assert.That(item, Is.Not.Null, $"Element with id '{itemAutomationId}' was not found.");
		item.Click();
		Wait(2000);
	}

	protected void ClickTopAppBarLeadingIcon()
	{
		var topAppBar_LeadingIcon = FindUIElementById("topAppBar_LeadingIcon");
		Assert.That(topAppBar_LeadingIcon, Is.Not.Null, "MaterialIconButton with id 'topAppBar_LeadingIcon' was not found.");
		topAppBar_LeadingIcon.Click();
		Wait(2000);
	}
}