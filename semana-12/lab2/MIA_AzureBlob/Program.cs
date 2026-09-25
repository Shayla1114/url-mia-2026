using Azure.Storage.Blobs;

string? connectionString =
    Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Error: No se encontro la Connection String.");
    return;
}

string containerName = "mia-archivos";

BlobServiceClient blobServiceClient =
    new BlobServiceClient(connectionString);

BlobContainerClient containerClient =
    blobServiceClient.GetBlobContainerClient(containerName);

Console.WriteLine("Conexion con Azure Blob Storage realizada correctamente.");

int opcion;

do
{
    Console.WriteLine();
    Console.WriteLine("=================================");
    Console.WriteLine("     MIA - AZURE BLOB STORAGE");
    Console.WriteLine("=================================");
    Console.WriteLine("1. Subir archivo");
    Console.WriteLine("2. Listar archivos");
    Console.WriteLine("3. Descargar archivo");
    Console.WriteLine("4. Eliminar archivo");
    Console.WriteLine("5. Salir");
    Console.WriteLine("=================================");
    Console.Write("Seleccione una opcion: ");

    if (!int.TryParse(Console.ReadLine(), out opcion))
    {
        opcion = 0;
    }

    switch (opcion)
    {
        case 1:
    Console.Write("Ingrese la ruta del archivo: ");
    string? rutaArchivo = Console.ReadLine();

    if (string.IsNullOrEmpty(rutaArchivo) || !File.Exists(rutaArchivo))
    {
        Console.WriteLine("El archivo no existe.");
        break;
    }

    string nombreArchivo = Path.GetFileName(rutaArchivo);

    BlobClient blobClient =
        containerClient.GetBlobClient(nombreArchivo);

    await blobClient.UploadAsync(rutaArchivo, overwrite: true);

    Console.WriteLine("Archivo subido correctamente.");
    Console.WriteLine($"Nombre: {nombreArchivo}");
    break;

        case 2:
    Console.WriteLine();
    Console.WriteLine("Nombre\t\t\tTamaño");
    Console.WriteLine("----------------------------------------");

    await foreach (var blobItem in containerClient.GetBlobsAsync())
    {
        Console.WriteLine(
            $"{blobItem.Name}\t\t{blobItem.Properties.ContentLength} bytes"
        );
    }

    break;

        case 3:
    Console.Write("Ingrese el nombre del archivo a descargar: ");
    string? nombreDescarga = Console.ReadLine();

    if (string.IsNullOrEmpty(nombreDescarga))
    {
        Console.WriteLine("Nombre no valido.");
        break;
    }

    BlobClient blobDescarga =
        containerClient.GetBlobClient(nombreDescarga);

    if (!await blobDescarga.ExistsAsync())
    {
        Console.WriteLine("El archivo no existe en Azure.");
        break;
    }

    Console.Write("Ingrese la carpeta de destino: ");
    string? carpetaDestino = Console.ReadLine();

    if (string.IsNullOrEmpty(carpetaDestino) ||
        !Directory.Exists(carpetaDestino))
    {
        Console.WriteLine("La carpeta de destino no existe.");
        break;
    }

    string rutaDestino =
        Path.Combine(carpetaDestino, nombreDescarga);

    await blobDescarga.DownloadToAsync(rutaDestino);

    Console.WriteLine("Archivo descargado correctamente.");
    Console.WriteLine($"Ubicacion: {rutaDestino}");
    break;

        case 4:
    Console.Write("Ingrese el nombre del archivo a eliminar: ");
    string? nombreEliminar = Console.ReadLine();

    if (string.IsNullOrEmpty(nombreEliminar))
    {
        Console.WriteLine("Nombre no valido.");
        break;
    }

    BlobClient blobEliminar =
        containerClient.GetBlobClient(nombreEliminar);

    if (!await blobEliminar.ExistsAsync())
    {
        Console.WriteLine("El archivo no existe en Azure.");
        break;
    }

    Console.Write($"¿Desea eliminar {nombreEliminar}? (S/N): ");
    string? confirmacion = Console.ReadLine();

    if (confirmacion?.ToUpper() == "S")
    {
        await blobEliminar.DeleteAsync();
        Console.WriteLine("Archivo eliminado correctamente.");
    }
    else
    {
        Console.WriteLine("Eliminacion cancelada.");
    }

    break;

        case 5:
            Console.WriteLine("Saliendo del programa...");
            break;

        default:
            Console.WriteLine("Opcion no valida.");
            break;
    }

} while (opcion != 5);
