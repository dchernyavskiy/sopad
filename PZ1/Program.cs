using System.Text;

const string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
const int shift = 9;
const string encryptedText1 =
    @"ъфчкч ""шчщы"", ср учычщчлч хчпцч йдфч шчцзые, аыч нлч шщчэнъъсз уиу-ыч ъкзрици ъ хчщнх. Ынщхсц ""уийнфе""";

Console.WriteLine(Task1.Decrypt(encryptedText1, shift, alphabet));
Console.WriteLine("\n################################################################\n");

const string encryptedText2 =
    @"прщуым ъй епръвцниф ёдой эюцмы уихумлфгя апс йамфщ у 1984 гопь. ан пьнхпоччшил, гыа есчс ты пъзуиллън воухажнъъдь иэшальучуатз к ьачръдве ъыьрыючфо кчжиа ишз ъли ычитонды адьнг алфъм, то иыа лидсэо бж ъэожщьп пръяцдуьь сутрцдиффусциф кгякъла смжъэа. дъффое нщцмя фмця шлхъра ъъдавлфссь нъцго чсйь кьигивът ьриыыагрлэъчеэуай гъфавоччюкох, ца в 2000 гъме, бллладаьз аднът ъзвръднох ьрзвфхастф к оллфшдичръьой цщъптълваффс, ъдей ьхалъън воыфатиюе у жиуцн.";
const string key = "алиса";
Console.WriteLine(Task2.Decrypt(encryptedText2, key, alphabet));
Console.WriteLine("\n################################################################\n");

const string encryptedText3 =
    @"огымыцерчиаан ьгиюдбгясжин геаъс плуфтнбянм яцкеьъчм цсцаёъ гаабгетцюеьъс къпйеш. яб коь птэ итсба уырсчт, ба йтэ ббмэфюо вгерояытк ацнв бгопэчмв, геаъа врчиынэы фоцяыкьафеьъс дяехош.";
const string key2 = "оста";

Console.WriteLine(Task2.Decrypt(encryptedText3, key2, alphabet));

class Task1
{
    private static string Encode(string encryptedText, int shift, string alphabet)
    {
        var normalizedAlphabet = alphabet.ToUpper();
        var count = normalizedAlphabet.Length;
        var sb = new StringBuilder();
        foreach (var c in encryptedText)
        {
            var index = normalizedAlphabet.IndexOf(char.ToUpper(c));
            if (index < 0)
                sb.Append(c);
            else
            {
                var codeIndex = (count + index + shift) % count;
                sb.Append(char.IsUpper(c)
                    ? char.ToUpper(normalizedAlphabet[codeIndex])
                    : char.ToLower(normalizedAlphabet[codeIndex]));
            }
        }

        return sb.ToString();
    }

    public static string Encrypt(string encryptedText, int shift, string alphabet)
        => Encode(encryptedText, shift, alphabet);

    public static string Decrypt(string encryptedText, int shift, string alphabet)
        => Encode(encryptedText, -shift, alphabet);
}

class Task2
{
    private static string AlignKey(string key, string text)
    {
        StringBuilder repeatedKey = new();
        for (int i = 0; i < text.Length; i++)
        {
            repeatedKey.Append(key[i % key.Length]);
        }

        return repeatedKey.ToString();
    }

    public static string Decrypt(string encryptedText, string key, string alphabet)
    {
        if (encryptedText.Length > key.Length)
        {
            key = AlignKey(key, encryptedText);
        }

        StringBuilder sb = new();
        for (int i = 0, j = 0; i < encryptedText.Length; i++)
        {
            char currentChar = encryptedText[i];
            if (alphabet.Contains(char.ToUpper(currentChar)))
            {
                int encryptedIndex = alphabet.IndexOf(char.ToUpper(currentChar));
                int keyIndex = alphabet.IndexOf(char.ToUpper(key[j % key.Length]));
                int textIndex = (encryptedIndex - keyIndex + alphabet.Length) % alphabet.Length;
                char decryptedChar =
                    char.IsLower(currentChar) ? char.ToLower(alphabet[textIndex]) : alphabet[textIndex];
                sb.Append(decryptedChar);
                j++;
            }
            else
            {
                sb.Append(currentChar);
            }
        }

        return sb.ToString();
    }
}