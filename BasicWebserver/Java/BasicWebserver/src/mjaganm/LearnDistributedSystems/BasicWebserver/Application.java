package mjaganm.LearnDistributedSystems.BasicWebserver;

import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class Application
{
    public static BasicWebServer webServer = null;

    @Bean
    public BasicWebServer getBasicWebServer()
    {
        return webServer;
    }

    public void setBasicWebServer(BasicWebServer basicWebServer)
    {
        webServer = basicWebServer;
    }
}
