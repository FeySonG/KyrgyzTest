using System.Text.RegularExpressions;
using KyrgyzTest.Application.Contracts.TextWorks.TextAnalysis;

namespace KyrgyzTest.Application.TextWorks.TextManipulation;

public class FrecuencyAnalyze
{
    public static TextAnalysisDto Analyze(string text)
    {
        // Удаляем знаки пунктуации и приводим к нижнему регистру
        string cleaned = Regex.Replace(text.ToLower(), @"[^\w\s]", "");

        // Разбиваем на слова и исключаем короткие (1 буква)
        string[] words = cleaned
            .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Where(w => w.Length > 1)
            .ToArray();

        // Частотный словарь (группировка и подсчёт)
        var frequency = words
            .GroupBy(w => w)
            .ToDictionary(g => g.Key, g => g.Count());

        // Можно дополнительно отсортировать словарь по убыванию
        var sortedFrequency = frequency
            .OrderByDescending(x => x.Value)
            .ToDictionary(x => x.Key, x => x.Value);

        var result = new TextAnalysisDto
        {
            WordCount = words.Length,
            CharCount = Regex.Matches(text, @"[A-Za-zА-Яа-я0-9]").Count,
            Text = text,
            Words = words.Distinct().ToList(),
            Frequency = sortedFrequency 
        };
        
        return result;
    }
}