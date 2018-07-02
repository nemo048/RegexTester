using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegexTester
{
	public partial class RegexTesterMainForm : Form
	{
		public static string OriginalText;

		private static string BasePattern = ".*";

		public RegexTesterMainForm()
		{
			InitializeComponent();
		}

		private void StartTestButton_Click(object sender, EventArgs e)
		{
			MatchValuesDataGridView.Rows.Clear();

			string pattern = TextOfRegexRichTextBox.Text;

			string textForTesting = TextForTestingRichTextBox.Text;

			if(TryGetValue(Regex.Matches, textForTesting, pattern, out MatchCollection matches))
			{
				for (int i = 0; i < matches.Count; i++)
				{
					MatchValuesDataGridView.Rows.Add(i, matches[i].Value);
				}
			}			
		}

		private bool TryGetValue<T>(Func<string, string, T> func, string arg1, string arg2, out T result)
		{
			try
			{
				result = func(arg1, arg2);
				return true;
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
				result = default(T);
				return false;
			}
		}

		private void ShowSpecialSymbolsCheckBox_CheckStateChanged(object sender, EventArgs e)
		{
			TextForTestingRichTextBox.WordWrap = ShowSpecialSymbolsCheckBox.Checked;

			if(ShowSpecialSymbolsCheckBox.Checked)
			{
				TextForTestingRichTextBox.Text = TextForTestingRichTextBox.Text.ShowSpecialSymbols();
				TextForTestingRichTextBox.ReadOnly = false;
			}
			else
			{
				TextForTestingRichTextBox.Text = TextForTestingRichTextBox.Text.DontShowSpecialSymbols();
				TextForTestingRichTextBox.ReadOnly = true;
			}
		}

		private void ChangeSearchPattern(object sender, EventArgs e)
		{
			string beforeText = string.Empty;
			string startOfText = string.Empty;
			string afterText = string.Empty;
			string endOfText = string.Empty;
			string middleOfText = BasePattern;

			if(!string.IsNullOrEmpty(BeforeTextAlwaysIsTextBox.Text))
			{
				beforeText = $"(?<={@BeforeTextAlwaysIsTextBox.Text.ShowSpecialSymbols()})";
			}

			if (!string.IsNullOrEmpty(TextAlwaysBeginsWithTextBox.Text))
			{
				startOfText = TextAlwaysBeginsWithTextBox.Text.ShowSpecialSymbols();
			}

			if (!string.IsNullOrEmpty(AfterTextAlwaysIsTextBox.Text))
			{
				afterText = $"(?={AfterTextAlwaysIsTextBox.Text.ShowSpecialSymbols()})";
			}

			if (!string.IsNullOrEmpty(TextAlwaysEndsWithTextBox.Text.ShowSpecialSymbols()))
			{
				endOfText = TextAlwaysEndsWithTextBox.Text;
			}

			if(AllowTransfersCheckBox.Checked)
			{
				middleOfText = middleOfText.Replace(".", @"[\w\W]");
			}

			if(TheShortestMatchCheckBox.Checked)
			{
				middleOfText += "?";
			}

			TextOfRegexRichTextBox.Text = $"{beforeText}{startOfText}{middleOfText}{endOfText}{afterText}";
		}
	}
}
