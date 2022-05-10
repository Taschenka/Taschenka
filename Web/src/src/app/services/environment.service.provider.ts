import { EnvironmentService } from './environment.service';

export const EnvironmentServiceFactory = () => {
  // Create env
  const env = new EnvironmentService();

  // Read environment variables from browser window
  const browserWindow = window || {};
  const browserWindowEnv = browserWindow[<any>'__environment'] || {};

  // Assign environment variables from browser window to env
  // In the current implementation, properties from env.js overwrite defaults from the EnvService.
  // If needed, a deep merge can be performed here to merge properties instead of overwriting them.
  for (const key in browserWindowEnv) {
    if (browserWindowEnv.hasOwnProperty(key)) {
      let value = window[<any>'__environment'][key] as any as string;
      env[key as keyof EnvironmentService] = value;
    }
  }

  return env;
};

export const EnvironmentServiceProvider = {
  provide: EnvironmentService,
  useFactory: EnvironmentServiceFactory,
  deps: [],
};
