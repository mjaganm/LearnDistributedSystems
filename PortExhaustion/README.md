


Port Exhaustion sample:

 - Build in Visual Studio
 - Run as Dotnet Core webserver rather than IIS server
 - Defualt Url: /weatherforecast
 - Do Port Exhaustion: /portexhaustion
 
 
 How it works?
  - A new middleware class is generated called PortExhaustion
  - This middleware class intercepts all requests received by the Webserver
  - For all other requests other than "/portexhaustion", they are processed as usual
  - For "/portexhaustion", a new request is generated on the same webserver with a HttpClient
  - Each request cascades into infinite loop of HttpRequests causing "Port Exhaustion" on the current machine
  
  - Check the used ports using "netstat" in a cmd folder

