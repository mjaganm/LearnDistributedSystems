package mjaganm.LearnDistributedSystems.BasicWebserver;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class BasicWebserverController
{
    @Autowired
    private BasicWebServer webServer;

    @RequestMapping(value = "/GetRandomInt/{intend}", method = RequestMethod.GET)
    private int GetRandomInt(@PathVariable int intend) {
        return webServer.GetRandomInt(intend);
    }

    @RequestMapping(value = "/GetRandomString", method = RequestMethod.GET)
    private String GetRandomString() {
        return webServer.GetRandomString();
    }

    @RequestMapping(value = "/GetRandomString/{length}", method = RequestMethod.GET)
    private String GetRandomString(@PathVariable int length) {
        return webServer.GetRandomString(length);
    }
}
