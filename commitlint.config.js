module.exports = {
	extends: ['@commitlint/config-conventional'],
	rules: {
		'body-leading-blank': [1, 'never'],

		'type-case': [2, 'always', 'lower-case'],
		'type-empty': [2, 'never'],
		'type-enum': [
			2,
			'always',
			[
				'build',
				'chore',
				'ci',
        'devops',
				'docs',
				'feat',
				'fix',
				'perf',
				'refactor',
				'revert',
				'style',
				'test',
				'any'
			]
		],

		'scope-case': [2, 'always', 'lower-case'],

		'subject-case': [
			2,
			'always',
			['sentence-case']
		],
		'subject-empty': [2, 'never']
	}
};	