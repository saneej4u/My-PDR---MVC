import { EZPickWebPage } from './app.po';

describe('ezpick-web App', function() {
  let page: EZPickWebPage;

  beforeEach(() => {
    page = new EZPickWebPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
