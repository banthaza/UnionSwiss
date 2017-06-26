import { UnionSwissWebPage } from './app.po';

describe('union-swiss-web App', () => {
  let page: UnionSwissWebPage;

  beforeEach(() => {
    page = new UnionSwissWebPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
