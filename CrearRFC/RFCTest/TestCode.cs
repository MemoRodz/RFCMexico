using ObtenRFC;

string APaterno = string.Empty;
string AMaterno = string.Empty;
string Nombres = string.Empty;
string FNac = string.Empty;
string RFC = string.Empty;

APaterno = "Rodriguez";
AMaterno = "Sarmiento";
Nombres = "Raul Guillermo";
FNac = "04/02/1975";

Console.WriteLine("RFC obtenido:\n");
RFC = obtieneRFC.CalcularRFC(Nombres, APaterno, AMaterno, FNac);
Console.WriteLine($"R. F. C.:\t{RFC}");
Console.ReadKey();

