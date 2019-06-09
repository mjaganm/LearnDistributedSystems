package mjaganm.LearnDistributedSystems.BasicWebserver;

import com.microsoft.azure.documentdb.DocumentClientException;
import org.apache.commons.lang3.RandomStringUtils;
import org.springframework.boot.SpringApplication;

import java.util.Random;


public class BasicWebServer {
    private static Random rand = new Random();

    public BasicWebServer(String[] args) {
    }

    public int GetRandomInt(int intend) {
        return rand.nextInt(intend);
    }

    public String GetRandomString() {
        return GetRandomString(25);
    }

    public String GetRandomString(int length) {
        return RandomStringUtils.randomAlphanumeric(length);
    }

    public static void main(String[] args) throws DocumentClientException
    {
        // Initialize BasicWebServer
        BasicWebServer webServer = new BasicWebServer(args);

        Application app = new Application();
        app.setBasicWebServer(webServer);

        // Run the Spring webserver
        SpringApplication.run(Application.class, args);
    }
}
