import { expect, test } from '@playwright/test';

test("index page: search name does exist", async ({ page }) => {
	
	await Promise.all([
		page.waitForResponse(
			response => 
				response.status() == 200 
				&& response.url().includes("/products")
		),
		await page.goto('/')
	]);

	await Promise.all([
		page.waitForResponse(
			response => 
				response.status() == 200 
				&& response.url().includes("/products?name=")
		),
		await page.locator('[placeholder="Search"]').fill('dolly'),
		await page.locator('button:has-text("Search")').click(),
	]);
	
	expect(Number(await page.textContent('id=product-count'))).toEqual(1);
});