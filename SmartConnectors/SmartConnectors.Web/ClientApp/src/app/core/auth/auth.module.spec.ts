import { AuthModule } from '~/core/auth/auth.module';

describe('AuthModule', () => {
  let authModule: AuthModule;

  beforeEach(() => {
    authModule = new AuthModule();
  });

  it('should create an instance', () => {
    expect(authModule).toBeTruthy();
  });
});
