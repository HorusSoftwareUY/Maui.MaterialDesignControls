using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using HorusStudio.Maui.MaterialDesignControls.UITests.Utils;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace HorusStudio.Maui.MaterialDesignControls.UITests;

public abstract class BaseTest
{
	protected AppiumDriver App => AppiumSetup.App;
	
	protected AppiumElement FindUIElement(string id)
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
			Logger.LogInfo($"{nameof(FindUIElement)} - Id: {id} - {nameof(NoSuchElementException)}: {ex.Message}");
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
		Task.Delay(500).Wait();
	}

	protected void ClickNavigationDrawerItem(string itemAutomationId)
	{
		FindUIElement("menu").Click();
		Wait(2000);
	    
		ScrollToElement(itemAutomationId);
		Wait(500);
		
		FindUIElement(itemAutomationId).Click();
		Wait(2000);
	}

	protected void ClickTopAppBarLeadingIcon()
	{
		FindUIElement("topAppBar_LeadingIcon").Click();
		Wait(2000);
	}
}