package com.utilidades.configuracion;

import org.springframework.context.annotation.*;
import oracle.jdbc.pool.OracleDataSource;
import javax.sql.DataSource;
import java.sql.SQLException;
import java.util.Arrays;
import java.util.List;
import org.slf4j.*;

@Configuration
@Profile("oracle")
public class OracleConfiguration {

    private Logger logger = LoggerFactory.getLogger(OracleConfiguration.class);

    @Bean
    @Lazy
    public DataSource dataSource() throws SQLException {
        String urldb = System.getenv("URLDB");

        if (urldb == null || urldb.isEmpty()) {
            throw new IllegalArgumentException("Environment variable URLDB null");
        }

        OracleDataSource dataSource = new OracleDataSource();
        dataSource.setURL(urldb);
        dataSource.setDriverType("oracle.jdbc.driver.OracleDriver");
        dataSource.setFastConnectionFailoverEnabled(true);
        dataSource.setImplicitCachingEnabled(true);
        return dataSource;
    }

    @Bean
    public List<String> users() {
        String usersEnv = System.getenv("USERS");
        if (usersEnv == null || usersEnv.isEmpty()) {
            throw new IllegalArgumentException("Environment variable USERS null");
        }
        return Arrays.asList(usersEnv.split(","));
    }

    @Bean
    public List<String> nicknames() {
        String nicknamesEnv = System.getenv("SECRET");
        if (nicknamesEnv == null || nicknamesEnv.isEmpty()) {
            throw new IllegalArgumentException("Environment variable SECRET null");
        }
        return Arrays.asList(nicknamesEnv.split(","));
    }
}
