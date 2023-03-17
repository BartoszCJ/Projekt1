using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;

namespace ProjektNr1
{


    public class Foo
    {
        //Mapowanie INDEXAMI
        [Index(0)]
        public string Lp { get; set; }
        [Index(1)]
        public string Nazwa_Własna { get; set; }
        [Index(2)]
        public string Telefon { get; set; }
        [Index(3)]
        public string Email { get; set; }
        [Index(4)]
        public string Charakter_Uslug { get; set; }
        [Index(5)]
        public string Kategoria_Obiektu { get; set; }
        [Index(6)]
        public string Rodzaj_Obiektu { get; set; }
        [Index(7)]
        public string Adres { get; set; }

    }

 
    public class Program
    {
            static void Main(string[] args)
            {

            //Console.OutputEncoding = Encoding.UTF8;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                    Delimiter = ";",
                    };


            //1.Wczytać dane z pliku hotele.csv z użyciem biblioteki CsvHelper
            using (var reader = new StreamReader(@"D:\Programowanie\ProjektNr1\hotele.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                var dane = csv.GetRecords<Foo>().ToList();


            //2. Wyszukać wszystkie hotele, których nazwa zaczyna się na literę 's'
                var hotelS = dane.Where(x => x.Nazwa_Własna.StartsWith("S"));
                foreach (var item in hotelS)
                {
                    Console.WriteLine($"Lp: {item.Lp}, Nazwa własna: {item.Nazwa_Własna}, Telefon: {item.Telefon}, Email: {item.Email}, Charakter usług: {item.Charakter_Uslug}, Kategoria obiektu: {item.Kategoria_Obiektu}, Rodzaj obiektu: {item.Rodzaj_Obiektu}, Adres: {item.Adres}");
                }


            //3. Obliczyć ile hoteli ma charakter sezonowy
                var sezonHotel = dane.Count(x => x.Charakter_Uslug.ToLower() == "sezonowy");
                Console.WriteLine($"Liczba hoteli z charakterem sezonowym: {sezonHotel}");


            //4. Wyświetlić wszystkie typy charakterów usług bez powtórzeń
                var uslugiTypy = dane.Select(r => r.Charakter_Uslug).Distinct();
                Console.WriteLine("Typy hoteli bez powtorzen:");
                foreach (var item in uslugiTypy)
                {
                    Console.WriteLine(item);
                }


            //5. Wyświetlić wszystkie kategorie hoteli bez powtórzeń
                var hoteleTypy = dane.Select(r => r.Kategoria_Obiektu).Distinct();

                Console.WriteLine("Kategorie hoteli bez powtorzen:");
                foreach (var item in hoteleTypy)
                {
                    Console.WriteLine(item);
                }

                
            //6. Wyświetlić hotele, które pochodzą z okolicy Bielska-Białej (numer telefonu zaczyna się 33)
                var bielskoHotele = dane.Where(x => x.Telefon.StartsWith("33"));
                
                Console.WriteLine("Hotele z okolicy Bielska-Białej:");
               
                foreach (var item in bielskoHotele)
                {
                    Console.WriteLine($"- {item.Nazwa_Własna}");
                }


            //7.Pogrupować hotele wg kategorii i zwrócić ile hoteli występuje w każdej grupie

                var grupowanieKategoria = dane.GroupBy(x => x.Kategoria_Obiektu);
                Console.WriteLine("\nLiczba hoteli wg kategorii:");
                foreach (var item in grupowanieKategoria)
                {
                    Console.WriteLine($"{item.Key}: {item.Count()}");
                }

            //8. Pogrupować hotele wg charakteru usług i zwrócić ile hoteli występuje w każdej grupie
                var grupowanieCharakter = dane.GroupBy(x => x.Charakter_Uslug);
                Console.WriteLine("\nLiczba hoteli wg charakteru usług:");
                foreach (var item in grupowanieCharakter)
                {
                    Console.WriteLine($"{item.Key}: {item.Count()}");
                }
            }
        }
    }
}