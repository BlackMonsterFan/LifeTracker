using System;
using System.Text.Json;
using Spectre.Console;

AnsiConsole.MarkupLine("[bold orange1]LOL HELLO[/]");

AnsiConsole.Write(new Rule("[bold red]ERROR[/]").RuleStyle("grey").LeftJustified());
AnsiConsole.Write(new Rule().RuleStyle("grey")); // Просто лінія

var quote = new Panel("No longer human, but becoming a machine.")
{
    Border = BoxBorder.Rounded,
    Padding = new Padding(1, 1, 1, 1),
    Header = new PanelHeader("[yellow] The Oracle [/]")
};

AnsiConsole.Write(quote);

var table = new Table().Border(TableBorder.Square).BorderColor(Color.Orange1);

table.AddColumn("[grey]Stat[/]");
table.AddColumn("[grey]Value[/]");

table.AddRow("Intelligence", "[cyan]500 XP[/]");
table.AddRow("Carbohydrates", "[green]450g[/] [grey](Optimal)[/]");
table.AddRow("Monsters", "[bold red]4 CANS (OVERDRIVE)[/]");

AnsiConsole.Write(new Align(table, HorizontalAlignment.Center));
