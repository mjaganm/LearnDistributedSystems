package mjaganm.LearnDistributedSystems.BasicWebserver;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.concurrent.atomic.AtomicLong;

// REST controller for "IsAlive" request mapping
@RestController
public class IsAliveController
{
    private static final String template = "Hello, %s!";
    private final AtomicLong counter = new AtomicLong();

    @RequestMapping("/isalive")
    public IsAlive IsAlive(@RequestParam(value="name", defaultValue="World") String name) {
        return new IsAlive(counter.incrementAndGet(),
                            String.format(template, name));
    }
}
