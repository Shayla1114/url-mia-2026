# Laboratorio No. 2 - Semana 12
## Cloud Storage - Azure Blob Storage

### Manejo e Implementación de Archivos

## Objetivo

Desarrollar una aplicación de consola en C# que permita conectarse a Azure Blob Storage mediante una Connection String y realizar las operaciones básicas de subir, listar, descargar y eliminar archivos almacenados en la nube.

## Investigación previa

### 1. ¿Qué es Azure Blob Storage?

Azure Blob Storage es un servicio de almacenamiento en la nube de Microsoft Azure que permite guardar grandes cantidades de datos no estructurados, como documentos, imágenes, videos, archivos de texto y copias de seguridad.

### 2. ¿Qué es un Storage Account?

Un Storage Account es un recurso de Azure que proporciona acceso a los servicios de almacenamiento. Dentro de este se pueden crear contenedores para organizar y almacenar los archivos utilizados por una aplicación.

### 3. ¿Qué es un Blob Container?

Un Blob Container es un espacio dentro de Azure Blob Storage utilizado para organizar un conjunto de blobs. Puede entenderse de forma sencilla como un contenedor en el que se agrupan los archivos que serán almacenados.

### 4. ¿Qué es un Blob?

Un Blob es un objeto o archivo almacenado dentro de Azure Blob Storage. Puede corresponder a un archivo de texto, imagen, documento PDF, video u otro tipo de información no estructurada.

### 5. ¿Qué es una Connection String?

Una Connection String es una cadena que contiene la información necesaria para que una aplicación pueda establecer una conexión con un servicio de Azure Storage.

### 6. ¿Qué información contiene una Connection String?

Una Connection String puede contener información como el protocolo utilizado para la conexión, el nombre de la cuenta de almacenamiento, la clave de acceso y los puntos de conexión necesarios para acceder a los servicios de Azure Storage.

Debido a que contiene información sensible, no debe almacenarse directamente en el código fuente ni publicarse en repositorios como GitHub.

### 7. ¿Qué función cumplen BlobServiceClient, BlobContainerClient y BlobClient?

- `BlobServiceClient`: permite establecer la comunicación con el servicio de Azure Blob Storage y acceder a los contenedores disponibles.
- `BlobContainerClient`: permite trabajar con un contenedor específico y consultar los blobs almacenados dentro de él.
- `BlobClient`: permite trabajar con un blob específico para realizar operaciones como subir, descargar, verificar su existencia o eliminarlo.

### 8. ¿Qué métodos del SDK permiten subir, listar, descargar y eliminar blobs?

En el SDK `Azure.Storage.Blobs` se pueden utilizar los siguientes métodos:

- `UploadAsync()`: permite subir un archivo a Azure Blob Storage.
- `GetBlobsAsync()`: permite obtener y recorrer los blobs almacenados en un contenedor.
- `DownloadToAsync()`: permite descargar un blob a una ubicación local.
- `DeleteAsync()`: permite eliminar un blob.
- `ExistsAsync()`: permite comprobar si un blob existe antes de realizar una operación.

## Tecnologías utilizadas

Para el desarrollo del laboratorio se utilizaron las siguientes tecnologías:

- C#
- .NET
- Microsoft Azure
- Azure Blob Storage
- SDK `Azure.Storage.Blobs`
- Visual Studio Code
- Git
- GitHub

## Configuración de Azure

Para realizar la práctica se utilizó un Storage Account de Microsoft Azure.

Dentro del Storage Account se creó un contenedor llamado:

`mia-archivos`

El contenedor fue configurado con nivel de acceso privado, evitando que los archivos almacenados puedan ser consultados de forma anónima.

La aplicación se conecta al servicio mediante una Connection String obtenida desde las claves de acceso del Storage Account.

## Arquitectura de la solución

La solución está formada por una aplicación de consola desarrollada en C# que se comunica con Azure Blob Storage utilizando el SDK `Azure.Storage.Blobs`.

La comunicación se realiza de la siguiente manera:

Usuario → Aplicación de consola C# → Azure.Storage.Blobs → Storage Account → Container `mia-archivos` → Blobs

Para realizar esta comunicación se utilizan principalmente las clases:

- `BlobServiceClient`
- `BlobContainerClient`
- `BlobClient`

`BlobServiceClient` establece la conexión con el servicio de almacenamiento. `BlobContainerClient` permite acceder al contenedor utilizado en la práctica y `BlobClient` permite trabajar con cada archivo almacenado.

## Operaciones implementadas

La aplicación presenta un menú con cuatro operaciones principales para administrar los archivos almacenados en Azure Blob Storage.

### Subir archivo

El usuario ingresa la ruta de un archivo almacenado localmente.

La aplicación verifica que el archivo exista, obtiene su nombre mediante `Path.GetFileName()` y crea un `BlobClient` para realizar la carga hacia el contenedor.

La carga se realiza mediante el método `UploadAsync()`.

Al finalizar, el programa muestra un mensaje indicando que el archivo fue subido correctamente.

### Listar archivos

La aplicación consulta los blobs almacenados dentro del contenedor mediante `GetBlobsAsync()`.

Por cada archivo encontrado se muestra:

- Nombre del archivo.
- Tamaño del archivo en bytes.

Esto permite comprobar desde la aplicación cuáles archivos se encuentran actualmente almacenados en Azure.

### Descargar archivo

El usuario ingresa el nombre del archivo que desea descargar.

La aplicación obtiene el `BlobClient` correspondiente y utiliza `ExistsAsync()` para verificar que el archivo realmente exista en Azure.

Posteriormente solicita una carpeta de destino y verifica que esta exista en el equipo.

Finalmente, el archivo se descarga utilizando `DownloadToAsync()` y se muestra la ubicación en la que fue guardado.

### Eliminar archivo

El usuario ingresa el nombre del archivo que desea eliminar.

Antes de realizar la operación, la aplicación verifica mediante `ExistsAsync()` que el blob se encuentre almacenado en Azure.

Después solicita una confirmación al usuario.

Si el usuario confirma la operación con la letra `S`, el archivo se elimina mediante `DeleteAsync()`. Si no confirma, la eliminación es cancelada.

## Manejo de errores y validaciones

La aplicación cuenta con diferentes validaciones para reducir errores durante su ejecución.

Entre las principales se encuentran:

- Validación de la existencia del archivo local antes de subirlo.
- Validación del nombre ingresado por el usuario.
- Comprobación de la existencia de un blob antes de descargarlo.
- Comprobación de la existencia de un blob antes de eliminarlo.
- Validación de la carpeta de destino antes de realizar una descarga.
- Validación de las opciones ingresadas en el menú.
- Confirmación antes de eliminar un archivo.
- Verificación de que exista una Connection String antes de establecer la conexión con Azure.

## Protección de la Connection String

La Connection String contiene información sensible que permite acceder al Storage Account, por lo que no fue escrita directamente dentro del código fuente.

Para protegerla se utilizó una variable de entorno llamada:

`AZURE_STORAGE_CONNECTION_STRING`

La aplicación obtiene el valor mediante:

`Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING")`

De esta manera, el código fuente únicamente contiene el nombre de la variable de entorno y no contiene la clave real de acceso.

Esto permite que el proyecto pueda almacenarse en GitHub sin publicar la Connection String ni las claves de acceso del Storage Account.

## Instrucciones para ejecutar el proyecto

### 1. Ingresar a la carpeta del proyecto

Desde una terminal se debe ingresar a la carpeta `MIA_AzureBlob`.

### 2. Instalar el paquete necesario

Si el paquete todavía no se encuentra instalado, ejecutar:

`dotnet add package Azure.Storage.Blobs`

### 3. Configurar la Connection String

En PowerShell se debe configurar la Connection String como una variable de entorno:

`$env:AZURE_STORAGE_CONNECTION_STRING="CONNECTION_STRING_DE_AZURE"`

Por seguridad, se debe sustituir el texto anterior por la Connection String real únicamente en la terminal. La credencial no debe escribirse dentro del código ni guardarse en GitHub.

### 4. Ejecutar la aplicación

Ejecutar:

`dotnet run`

El programa mostrará el siguiente menú:

1. Subir archivo
2. Listar archivos
3. Descargar archivo
4. Eliminar archivo
5. Salir

El usuario puede seleccionar la operación que desea realizar ingresando el número correspondiente.

## Pruebas realizadas

Durante la práctica se realizaron pruebas para verificar el funcionamiento de cada operación.

Se comprobó:

- La conexión de la aplicación con Azure Blob Storage.
- La carga de un archivo local al contenedor.
- La visualización del archivo desde Azure Portal.
- El listado de archivos desde la aplicación de C#.
- La descarga del archivo hacia el equipo local.
- La eliminación del archivo desde la aplicación.
- La eliminación correcta del archivo dentro del contenedor de Azure.

Con estas pruebas se verificó el funcionamiento de las cuatro operaciones principales implementadas en la aplicación.