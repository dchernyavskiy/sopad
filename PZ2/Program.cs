// See https://aka.ms/new-console-template for more information

using System.Text;

const string message = "57117823868830877701066169741847";
const string key = "одо";
var table = new Dictionary<string, char>
{
    { "1", 'А' }, { "2", 'И' }, { "3", 'Т' }, { "4", 'Е' }, { "5", 'С' }, { "6", 'Н' }, { "7", 'О' }, { "81", 'Б' },
    { "82", 'В' }, { "83", 'Г' }, { "84", 'Д' }, { "85", 'Ж' }, { "86", 'З' }, { "87", 'К' }, { "88", 'Л' },
    { "89", 'М' }, { "80", 'П' }, { "91", 'Р' }, { "92", 'У' }, { "93", 'Ф' }, { "94", 'Х' }, { "95", 'Ц' },
    { "96", 'Ч' }, { "97", 'Ш' }, { "98", 'Щ' }, { "99", 'Ъ' }, { "90", 'Ы' }, { "01", 'Ь' }, { "02", 'Э' },
    { "03", 'Ю' }, { "04", 'Я' }, { "05", 'Й' }, { "06", 'Ё' }, { "00", ' ' }
};
var decodedText = Decode(message, key, table);
Console.WriteLine(Textify(decodedText, table));

Console.WriteLine("\n################################################################\n");

const string message2 = "ФИО и день рождение";
const string key2 = "семнадцатый";
var encoded = Encode(message2, key2, table);
Console.WriteLine(encoded);
decodedText = Decode(encoded, key2, table);
Console.WriteLine(Textify(decodedText, table));


static string Digitalize(string key, Dictionary<string, char> table)
{
    return string.Join("",
        key.ToUpper().ToCharArray().Select(x => table.FirstOrDefault(pair => pair.Value == x).Key).ToArray());
}

static string AlignText(string key, string text)
{
    return new string(Enumerable.Range(0, text.Length)
        .Select(i => key[i % key.Length])
        .ToArray());
}

static string Decode(string message, string key, Dictionary<string, char> table)
{
    var digitalizedKey = Digitalize(key, table);
    var alignedKey = AlignText(digitalizedKey, message);
    var decodedText = new StringBuilder();
    for (int i = 0; i < message.Length; i++)
    {
        var num = int.Parse(message[i].ToString());
        var num2 = int.Parse(alignedKey[i].ToString());
        decodedText.Append((num - num2 + 10) % 10);
    }

    return decodedText.ToString();
}

static string Textify(string result, Dictionary<string, char> table)
{
    var resultBuilder = new StringBuilder();
    for (int i = 0; i < result.Length; i++)
    {
        var number = int.Parse(result[i].ToString());
        if (number <= 7 && number != 0)
        {
            resultBuilder.Append(table.FirstOrDefault(x => x.Key == result[i].ToString()).Value);
        }
        else if (number != 0 || i != result.Length - 1)
        {
            resultBuilder.Append(table.FirstOrDefault(x => x.Key == result[i].ToString() + result[i + 1].ToString())
                .Value);
            i++;
        }
    }

    return resultBuilder.ToString();
}

static string Encode(string message, string key, Dictionary<string, char> table)
{
    var digitalizedMessage = Digitalize(message, table);
    var digitalizedKey = Digitalize(key, table);
    var alignedKey = AlignText(digitalizedKey, digitalizedMessage);

    var encodedText = new StringBuilder();
    for (int i = 0; i < digitalizedMessage.Length; i++)
    {
        var num = int.Parse(digitalizedMessage[i].ToString());
        var num2 = int.Parse(alignedKey[i].ToString());
        encodedText.Append((num + num2) % 10);
    }

    return encodedText.ToString();
}